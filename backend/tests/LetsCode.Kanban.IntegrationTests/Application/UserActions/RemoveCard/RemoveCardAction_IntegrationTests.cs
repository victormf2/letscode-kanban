using System.Linq;
using AutoBogus;
using LetsCode.Kanban.Application.Exceptions;
using LetsCode.Kanban.Application.Models;
using LetsCode.Kanban.Application.UserActions.RemoveCard;
using LetsCode.Kanban.IntegrationTests.Common;
using LetsCode.Kanban.Persistence.EntityFrameworkCore;
using LetsCode.Kanban.TestHelpers.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Xunit;
using System.Collections.Generic;

namespace LetsCode.Kanban.IntegrationTests.Application.UserActions.RemoveCard
{
    public class RemoveCardAction_IntegrationTests : IntegrationTestsBase
    {
        private readonly RemoveCardAction _removeCardAction;
        private readonly ApplicationDbContext _dbContext;
        public RemoveCardAction_IntegrationTests(IntegrationTestsFixture integrationTestsFixture) : base(integrationTestsFixture)
        {
            _removeCardAction = ServiceProvider.GetService<RemoveCardAction>();
            _dbContext = ServiceProvider.GetService<ApplicationDbContext>();
        }

        [Fact]
        public async Task Must_throw_NotFoundException_for_inexisting_card_id()
        {
            await CreateAndAddCard();
            var parametersWithInexistingId = new AutoFaker<RemoveCardParameters>().Generate();

            Func<Task> executeRemoveCardAction = 
                () => _removeCardAction.Execute(parametersWithInexistingId);

            await Assert.ThrowsAsync<NotFoundException>(executeRemoveCardAction);
        }

        [Fact]
        public async Task Must_return_cards_list_on_success()
        {
            var initialCount = 5;
            var allCards = await EnsureExactly(initialCount).CardsAreAdded();
            var cardToRemove = allCards.First();

            var parameters = new AutoFaker<RemoveCardParameters>()
                .RuleFor(p => p.Id, cardToRemove.Id)
                .Generate();

            var result = await _removeCardAction.Execute(parameters);
            var expectedCountAfterRemove = initialCount - 1;

            Assert.NotNull(result?.Cards);
            Assert.Equal(expectedCountAfterRemove, result.Cards.Count);
            Assert.DoesNotContain(result.Cards, card => card.Id == cardToRemove.Id);

            foreach (var expectedCard in allCards.Except(new[] { cardToRemove }))
            {
                Assert.Contains(result.Cards, actualCard =>
                    expectedCard.Id == actualCard.Id &&
                    expectedCard.Title == actualCard.Title &&
                    expectedCard.Content == actualCard.Content &&
                    expectedCard.ListId == actualCard.ListId);
            }
        }

        [Fact]
        public async Task Must_remove_the_card_from_database()
        {
            var initialCount = 5;
            var allCards = await EnsureExactly(initialCount).CardsAreAdded();
            var cardToRemove = allCards.First();

            var parameters = new AutoFaker<RemoveCardParameters>()
                .RuleFor(p => p.Id, cardToRemove.Id)
                .Generate();

            await _removeCardAction.Execute(parameters);

            var expectedCountAfterRemove = initialCount - 1;
            var cardsNotRemoved = await _dbContext.Set<Card>().ToListAsync();

            Assert.Equal(expectedCountAfterRemove, cardsNotRemoved.Count);
            Assert.DoesNotContain(cardsNotRemoved, card => card.Id == cardToRemove.Id);
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

        private CardsGenerator EnsureExactly(int count)
            => new CardsGenerator(_dbContext, count);

        private class CardsGenerator
        {
            private readonly ApplicationDbContext _dbContext;
            private readonly int _expectedCount;

            public CardsGenerator(ApplicationDbContext dbContext, int expectedCount)
            {
                _dbContext = dbContext;
                _expectedCount = expectedCount;
            }

            public async Task<List<Card>> CardsAreAdded()
            {
                var existingCards = await _dbContext.Set<Card>().ToListAsync();
                var numberOfCardsToAdd = _expectedCount - existingCards.Count;
                
                var faker = new AutoFaker<Card>()
                    .RuleFor(c => c.ListId, f => f.ListId());

                var newCards = faker.GenerateForever().Take(numberOfCardsToAdd).ToList();

                _dbContext.AddRange(newCards);
                await _dbContext.SaveChangesAsync();

                var xx = _dbContext.Set<Card>().ToList();

                return existingCards.Concat(newCards).ToList();
            }
        }
    }
}