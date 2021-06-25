using System.Threading.Tasks;
using LetsCode.Kanban.Application.Core;
using LetsCode.Kanban.WebApi.ApplicationImplementations.Authentication;
using LetsCode.Kanban.WebApi.ApplicationImplementations.Core;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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

            var jwtConfigurationSection = Configuration.GetSection("Jwt");
            services.Configure<JwtGeneratorOptions>(jwtConfigurationSection);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    var jwtGeneratorOptions = jwtConfigurationSection.Get<JwtGeneratorOptions>();

                    var tokenValidationParameters = options.TokenValidationParameters;

                    tokenValidationParameters.ValidateIssuer = jwtGeneratorOptions.Issuer != null;
                    tokenValidationParameters.ValidIssuer = jwtGeneratorOptions.Issuer;

                    tokenValidationParameters.ValidateAudience = jwtGeneratorOptions.Audience != null;
                    tokenValidationParameters.ValidAudience = jwtGeneratorOptions.Audience;

                    tokenValidationParameters.RequireExpirationTime = jwtGeneratorOptions.ExpiresAfter != null;

                    options.Events.OnMessageReceived = ctx =>
                    {
                        if (ctx.Request.Cookies.TryGetValue("AuthJwt", out string jwt))
                        {
                            ctx.Token = jwt;
                        }
                        return Task.CompletedTask;
                    };
                });

            services.AddScoped<IActionContext, WebActionContext>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
            });
        }
    }
}
