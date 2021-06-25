using AutoBogus;
using LetsCode.Kanban.Application.Exceptions;
using LetsCode.Kanban.Application.Models;
using LetsCode.Kanban.Application.UserActions.UpdateCard;
using LetsCode.Kanban.IntegrationTests.Common;
using LetsCode.Kanban.Persistence.EntityFrameworkCore;
using LetsCode.Kanban.TestHelpers.Extensions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace LetsCode.Kanban.IntegrationTests.Application.UserActions.UpdateCard
{
    public class UpdateCardAction_IntegrationTests : IntegrationTestsBase, IClassFixture<IntegrationTestsFixture>
    {

        private readonly UpdateCardAction _updateCardAction;
        private readonly ApplicationDbContext _dbContext;

        public UpdateCardAction_IntegrationTests(IntegrationTestsFixture integrationTestsFixture) : base(integrationTestsFixture)
        {
            _updateCardAction = ServiceProvider.GetService<UpdateCardAction>();
            _dbContext = ServiceProvider.GetService<ApplicationDbContext>();
        }

        [Fact]
        public async Task Must_throw_NotFoundException_for_inexisting_card_id()
        {
            await CreateAndAddCard();

            var parametersWithInexistingId = new AutoFaker<UpdateCardParameters>()
                .RuleFor(p => p.ListId, f => f.ListId())
                .Generate();

            Func<Task> executeUpdateCardAction = 
                () => _updateCardAction.Execute(parametersWithInexistingId);

            await Assert.ThrowsAsync<NotFoundException>(executeUpdateCardAction);
        }

        [Fact]
        public async Task Must_return_complete_updated_card_on_success()
        {
            var card = await CreateAndAddCard();
            var parameters = GetValidUpdateCardParameters(card.Id);

            var result = await _updateCardAction.Execute(parameters);

            Assert.NotNull(result);
            Assert.Equal(card.Id, result.Id);
            Assert.Equal(parameters.Title, result.Title);
            Assert.Equal(parameters.Content, result.Content);
            Assert.Equal(parameters.ListId, result.ListId);
        }

        [Fact]
        public async Task Must_store_updated_card_in_database()
        {
            var card = await CreateAndAddCard();
            var parameters = GetValidUpdateCardParameters(card.Id);

            await _updateCardAction.Execute(parameters);

            var cardInDatabase = await _dbContext.FindAsync<Card>(card.Id);

            Assert.NotNull(cardInDatabase);
            Assert.Equal(parameters.Title, cardInDatabase.Title);
            Assert.Equal(parameters.Content, cardInDatabase.Content);
            Assert.Equal(parameters.ListId, cardInDatabase.ListId);
        }

        private static UpdateCardParameters GetValidUpdateCardParameters(Guid cardId)
        {
            return new AutoFaker<UpdateCardParameters>()
                .RuleFor(p => p.Id, cardId)
                .RuleFor(p => p.ListId, f => f.ListId())
                .Generate();
        }

        private async Task<Card> CreateAndAddCard()
        {
            var card = new AutoFaker<Card>()
                .RuleFor(c => c.ListId, f => f.ListId())
                .Generate();
                
            _dbContext.Add(card);
            await _dbContext.SaveChangesAsync();

            return card;
        }
    }
}