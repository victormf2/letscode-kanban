using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoBogus;
using LetsCode.Kanban.Application.Models;
using LetsCode.Kanban.Application.UserActions.ListAllCards;
using LetsCode.Kanban.IntegrationTests.Common;
using LetsCode.Kanban.Persistence.EntityFrameworkCore;
using LetsCode.Kanban.TestHelpers.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace LetsCode.Kanban.IntegrationTests.Application.UserActions.ListAllCards
{
    public class ListAllCardsAction_IntegrationTests : IntegrationTestsBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ListAllCardsAction _listAllCardsAction;

        public ListAllCardsAction_IntegrationTests(
            IntegrationTestsFixture integrationTestsFixture) : base(integrationTestsFixture)
        {
            _dbContext = ServiceProvider.GetService<ApplicationDbContext>();
            _listAllCardsAction = ServiceProvider.GetService<ListAllCardsAction>();
        }

        [Fact]
        public async Task Must_return_cards_list_on_success()
        {
            var expectedCount = 5;
            var allCards = await CreateAndAddCards(expectedCount);

            var result = await _listAllCardsAction.Execute();

            Assert.NotNull(result?.Cards);
            Assert.Equal(expectedCount, result.Cards.Count);

            foreach (var expectedCard in allCards)
            {
                Assert.Contains(result.Cards, actualCard =>
                    expectedCard.Id == actualCard.Id &&
                    expectedCard.Title == actualCard.Title &&
                    expectedCard.Content == actualCard.Content &&
                    expectedCard.ListId == actualCard.ListId);
            }
        }

        private async Task<List<Card>> CreateAndAddCards(int expectedCount)
        {
            var faker = new AutoFaker<Card>()
                            .RuleFor(c => c.ListId, f => f.ListId());

            var allCards = faker.GenerateForever().Take(expectedCount).ToList();
            _dbContext.AddRange(allCards);
            await _dbContext.SaveChangesAsync();
            return allCards;
        }
    }
}