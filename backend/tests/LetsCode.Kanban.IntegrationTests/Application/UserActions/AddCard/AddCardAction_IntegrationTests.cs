using AutoBogus;
using LetsCode.Kanban.Application.UserActions.AddCard;
using LetsCode.Kanban.IntegrationTests.Common;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Xunit;

namespace LetsCode.Kanban.IntegrationTests.Application.UserActions.AddCard
{
    public class AddCardAction_IntegrationTests : IClassFixture<WebApiServices>, IDisposable
    {
        private readonly IServiceScope _serviceScope;
        private readonly AddCardAction _addCardAction;
        public AddCardAction_IntegrationTests(WebApiServices webApiServices)
        {
            _serviceScope = webApiServices.CreateScope();
            _addCardAction = _serviceScope.ServiceProvider.GetService<AddCardAction>();
        }

        [Fact]
        public async Task Must_return_complete_card_on_success()
        {
            var parameters = new AutoFaker<AddCardParameters>()
                .RuleFor(p => p.ListId, f => f.PickRandom("ToDo", "Doing", "Done"))
                .Generate();

            var result = await _addCardAction.Execute(parameters);

            Assert.NotNull(result);
            Assert.NotEqual(default, result.Id);
            Assert.Equal(parameters.Title, result.Title);
            Assert.Equal(parameters.Content, result.Content);
            Assert.Equal(parameters.ListId, result.ListId);
        }

        public void Dispose()
        {
            _serviceScope.Dispose();
        }
    }
}