using System;
using LetsCode.Kanban.Application.Core;
using LetsCode.Kanban.WebApi;
using Microsoft.Extensions.DependencyInjection;

namespace LetsCode.Kanban.IntegrationTests.Common
{
    public class IntegrationTestsFixture
    {
        private readonly Lazy<IServiceProvider> _rootServiceProvider;
        private readonly IServiceCollection _services;
        
        public IntegrationTestsFixture()
        {
            _services = new ServiceCollection();
            var startup = new Startup();
            startup.ConfigureServices(_services);

            _services.AddScoped<TestActionContext>();
            _services.AddScoped<IActionContext>(serviceProvider => serviceProvider.GetService<TestActionContext>());

            _rootServiceProvider = new Lazy<IServiceProvider>(() => _services.BuildServiceProvider());

        }

        private bool _isConfigured;
        public void ConfigureServices(Action<IServiceCollection> configureAction)
        {
            if (!_isConfigured)
            {
                configureAction(_services);
                _isConfigured = false;
            }
        }

        public IServiceScope CreateScope()
        {
            return _rootServiceProvider.Value.CreateScope();
        }
    }
}