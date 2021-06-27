using System.Threading.Tasks;
using LetsCode.Kanban.Application.Authentication.Implementation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;

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
    }
}