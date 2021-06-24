using System;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace LetsCode.Kanban.IntegrationTests.Common
{
    public abstract class IntegrationTestsBase : IClassFixture<IntegrationTestsFixture>, IDisposable
    {
        private readonly IServiceScope _serviceScope;
        protected IServiceProvider ServiceProvider => _serviceScope.ServiceProvider;

        public IntegrationTestsBase(IntegrationTestsFixture integrationTestsFixture)
        {
            _serviceScope = integrationTestsFixture.CreateScope();
        }

        public void Dispose()
        {
            _serviceScope.Dispose();
        }
    }
}