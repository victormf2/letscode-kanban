using AutoBogus;
using LetsCode.Kanban.Application.Models;
using LetsCode.Kanban.Application.UserActions.AddCard;
using LetsCode.Kanban.IntegrationTests.Common;
using LetsCode.Kanban.Persistence.EntityFrameworkCore;
using LetsCode.Kanban.TestHelpers.Extensions;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Xunit;

namespace LetsCode.Kanban.IntegrationTests.Application.UserActions.AddCard
{
    public class AddCardAction_IntegrationTests : IntegrationTestsBase
    {
        private readonly AddCardAction _addCardAction;
        private readonly ApplicationDbContext _dbContext;
        public AddCardAction_IntegrationTests(IntegrationTestsFixture integrationTestsFixture) : base(integrationTestsFixture)
        {
            _addCardAction = ServiceProvider.GetService<AddCardAction>();
            _dbContext = ServiceProvider.GetService<ApplicationDbContext>();
        }

        [Fact]
        public async Task Must_return_complete_card_on_success()
        {
            var parameters = GetValidAddCardParameters();

            var result = await _addCardAction.Execute(parameters);

            Assert.NotNull(result);
            Assert.NotEqual(default, result.Id);
            Assert.Equal(parameters.Title, result.Title);
            Assert.Equal(parameters.Content, result.Content);
            Assert.Equal(parameters.ListId, result.ListId);
        }

        [Fact]
        public async Task Must_store_card_in_database()
        {
            var parameters = GetValidAddCardParameters();

            var result = await _addCardAction.Execute(parameters);
            Assert.NotNull(result);

            var cardInDatabase = await _dbContext.FindAsync<Card>(result.Id);

            Assert.NotNull(cardInDatabase);
            Assert.Equal(parameters.Title, cardInDatabase.Title);
            Assert.Equal(parameters.Content, cardInDatabase.Content);
            Assert.Equal(parameters.ListId, cardInDatabase.ListId);
        }

        private static AddCardParameters GetValidAddCardParameters()
        {
            return new AutoFaker<AddCardParameters>()
                .RuleFor(p => p.ListId, f => f.ListId())
                .Generate();
        }
    }
}