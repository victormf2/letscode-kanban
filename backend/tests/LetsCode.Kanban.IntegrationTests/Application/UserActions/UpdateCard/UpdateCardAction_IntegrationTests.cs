using System;
using System.Threading.Tasks;
using AutoBogus;
using LetsCode.Kanban.Application.Models;
using LetsCode.Kanban.Application.UserActions.UpdateCard;
using LetsCode.Kanban.IntegrationTests.Common;
using LetsCode.Kanban.Persistence.InMemory;
using Microsoft.Extensions.DependencyInjection;
using LetsCode.Kanban.TestHelpers.Extensions;
using Xunit;
using LetsCode.Kanban.Application.Exceptions;

namespace LetsCode.Kanban.IntegrationTests.Application.UserActions.UpdateCard
{
    public class UpdateCardAction_IntegrationTests : IntegrationTestsBase, IClassFixture<UpdateCardAction_IntegrationTests.AddCardFixture>
    {

        private readonly UpdateCardAction _updateCardAction;
        private readonly InMemoryDbContext _dbContext;
        private readonly Guid _addedCardId;

        public UpdateCardAction_IntegrationTests(AddCardFixture addCardFixture) : base(addCardFixture)
        {
            _updateCardAction = ServiceProvider.GetService<UpdateCardAction>();
            _dbContext = ServiceProvider.GetService<InMemoryDbContext>();
            _addedCardId = addCardFixture.AddedCardId;
        }

        [Fact]
        public async Task Must_throw_NotFoundException_for_inexisting_card_id()
        {
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
            var parameters = GetValidUpdateCardParameters(_addedCardId);

            var result = await _updateCardAction.Execute(parameters);

            Assert.NotNull(result);
            Assert.Equal(_addedCardId, result.Id);
            Assert.Equal(parameters.Title, result.Title);
            Assert.Equal(parameters.Content, result.Content);
            Assert.Equal(parameters.ListId, result.ListId);
        }

        [Fact]
        public async Task Must_store_updated_card_in_database()
        {
            var parameters = GetValidUpdateCardParameters(_addedCardId);

            await _updateCardAction.Execute(parameters);

            var cardInDatabase = await _dbContext.FindAsync<Card>(_addedCardId);

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

        public class AddCardFixture : IntegrationTestsFixture
        {
            public Guid AddedCardId { get; }
            public AddCardFixture()
            {
                using var scope = CreateScope();
                var dbContext = scope.ServiceProvider.GetService<InMemoryDbContext>();

                var card = new AutoFaker<Card>()
                    .RuleFor(c => c.ListId, f => f.ListId())
                    .Generate();
                
                dbContext.Add(card);
                dbContext.SaveChanges();

                AddedCardId = card.Id;
            }
        }
    }
}