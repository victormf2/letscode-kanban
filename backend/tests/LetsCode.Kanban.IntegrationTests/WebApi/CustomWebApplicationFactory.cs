using LetsCode.Kanban.WebApi;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using System;

namespace LetsCode.Kanban.IntegrationTests.WebApi
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Startup>
    {
        public CustomWebApplicationFactory()
        {
            Environment.SetEnvironmentVariable("Jwt__Secret", "zEc2bC2Y1cnqkOgXRVw2E9nweF7XIoB5");
            Environment.SetEnvironmentVariable("Authentication__Username", "victor");
            Environment.SetEnvironmentVariable("Authentication__Password", "123456");
        }
        
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {

            builder
                .UseSolutionRelativeContentRoot("src/LetsCode.Kanban.WebApi");
        }
    }
}