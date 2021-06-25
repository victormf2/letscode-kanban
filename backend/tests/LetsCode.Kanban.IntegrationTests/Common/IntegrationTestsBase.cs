using System;
using LetsCode.Kanban.Persistence.EntityFrameworkCore.InMemory;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace LetsCode.Kanban.IntegrationTests.Common
{
    public abstract class IntegrationTestsBase : IClassFixture<IntegrationTestsFixture>, IDisposable
    {
        protected IntegrationTestsFixture _integrationTestsFixture;
        private readonly Lazy<IServiceScope> _serviceScope;
        protected IServiceProvider ServiceProvider => _serviceScope.Value.ServiceProvider;

        public IntegrationTestsBase(IntegrationTestsFixture integrationTestsFixture)
        {
            integrationTestsFixture.ConfigureServices(services =>
            {
                var databaseName = GetType().Name;
                services.Configure<InMemoryDatabaseOptions>(options =>
                {
                    options.Name = databaseName;
                });

                ConfigureServices(services);
            });

            _serviceScope = new Lazy<IServiceScope>(() => integrationTestsFixture.CreateScope());
            _integrationTestsFixture = integrationTestsFixture;
        }

        protected virtual void ConfigureServices(IServiceCollection services)
        {
        }

        public void Dispose()
        {
            if (_serviceScope.IsValueCreated)
            {
                _serviceScope.Value.Dispose();
            }
        }
    }
}