using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using LetsCode.Kanban.Application.Authentication.Implementation;
using LetsCode.Kanban.WebApi.Swagger.OperationFilters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static void AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration) {

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    var jwtGeneratorOptions = configuration.GetSection("Jwt").Get<JwtGeneratorOptions>(options =>
                    {
                        options.BindNonPublicProperties = true;
                    });

                    var tokenValidationParameters = options.TokenValidationParameters;

                    tokenValidationParameters.ValidateIssuer = jwtGeneratorOptions.Issuer != null;
                    tokenValidationParameters.ValidIssuer = jwtGeneratorOptions.Issuer;

                    tokenValidationParameters.ValidateAudience = jwtGeneratorOptions.Audience != null;
                    tokenValidationParameters.ValidAudience = jwtGeneratorOptions.Audience;

                    tokenValidationParameters.RequireExpirationTime = jwtGeneratorOptions.ExpiresAfter != null;

                    tokenValidationParameters.IssuerSigningKey = jwtGeneratorOptions.GetSigningKey();

                    options.Events = new JwtBearerEvents()
                    {
                        OnMessageReceived = ctx =>
                        {
                            if (ctx.Request.Cookies.TryGetValue("AuthJwt", out string jwt))
                            {
                                ctx.Token = jwt;
                            }
                            return Task.CompletedTask;
                        }
                    };
                });
        }

        public static void AddApiDocs(this IServiceCollection services)
        {

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Let's Code – Kanban",
                    Description = "A simple example ASP.NET Core Web API",
                    Contact = new OpenApiContact
                    {
                        Name = "Victor Machado de França",
                        Email = string.Empty,
                        Url = new Uri("https://github.com/victormf2"),
                    },
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                                Enter 'Bearer {JWT}'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.OperationFilter<AddSecurityRequirementOperationFilter>();

                c.CustomSchemaIds(type => type.FullName.Replace('+', '.'));

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }
    }
}