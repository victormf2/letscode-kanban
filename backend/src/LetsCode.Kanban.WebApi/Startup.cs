using LetsCode.Kanban.Application.Core;
using LetsCode.Kanban.Persistence.EntityFrameworkCore;
using LetsCode.Kanban.Persistence.EntityFrameworkCore.PostgreSql;
using LetsCode.Kanban.WebApi.ApplicationImplementations.Core;
using LetsCode.Kanban.WebApi.Filters;
using LetsCode.Kanban.WebApi.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;

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
            services.AddApiDocs();

            services.AddApplication(Configuration);

            var postgreConfiguration = Configuration.GetSection("Postgre");
            if (postgreConfiguration.Exists())
            {
                services.Configure<PostgreSqlOptions>(postgreConfiguration);
                services.AddPostgreSqlPersistence();
            }
            else
            {
                services.AddInMemoryPersistence();
            }

            services.AddJwtAuthentication(Configuration);
            services.AddAuthorization();

            services.AddHttpContextAccessor();
            services.AddControllers();

            services.AddScoped<LogCardActionAttribute>();

            services.AddScoped<IActionContext, WebActionContext>();

            services.AddScoped<ErrorHandlingMiddleware>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
                if (context is PostgreSqlApplicationDbContext) 
                {
                    context.Database.Migrate();
                }
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseCors(options => 
            {
                options
                    .WithOrigins(
                        Environment.GetEnvironmentVariable("CLIENT_HOST") ?? 
                        "http://localhost:4200")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });

            app.UseMiddleware<ErrorHandlingMiddleware>();

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
