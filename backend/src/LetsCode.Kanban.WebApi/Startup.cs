using LetsCode.Kanban.Application.Authentication;
using LetsCode.Kanban.Application.Authentication.Implementation;
using LetsCode.Kanban.Application.Core;
using LetsCode.Kanban.Application.Core.Implementation;
using LetsCode.Kanban.WebApi.ApplicationImplementations.Core;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;

namespace LetsCode.Kanban.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplication();
            services.AddInMemoryPersistence();

            var authenticationConfigurationSection = Configuration.GetSection("Authentication");
            services.Configure<LocalEnvironmentAuthenticatorOptions>(authenticationConfigurationSection);
            services.AddSingleton<IAuthenticator, LocalEnvironmentAuthenticator>();

            var jwtConfigurationSection = Configuration.GetSection("Jwt");
            services.Configure<JwtGeneratorOptions>(jwtConfigurationSection, options =>
            {
                options.BindNonPublicProperties = true;
            });
            services.AddSingleton<IJwtGenerator, JwtGenerator>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    var jwtGeneratorOptions = jwtConfigurationSection.Get<JwtGeneratorOptions>(options =>
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

            services.AddAuthorization();

            services.AddHttpContextAccessor();
            services.AddControllers();

            services.AddScoped<IActionContext, WebActionContext>();
            services.AddSingleton<IDateTime, UtcDateTime>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
