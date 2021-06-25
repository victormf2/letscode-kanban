using System.Linq;
using System.Threading.Tasks;
using LetsCode.Kanban.Application.Persistence;

namespace LetsCode.Kanban.Application.UserActions.ListAllCards
{
    public class ListAllCardsAction
    {
        private readonly ICardsRepository _cards;

        public ListAllCardsAction(ICardsRepository cards)
        {
            _cards = cards;
        }

        public async Task<ListAllCardsResult> Execute()
        {
            var allCards = await _cards.ListAll();

            return new ListAllCardsResult
            {
                Cards = allCards.Select(card => new ListAllCardsResult.Card
                {
                    Id = card.Id,
                    Title = card.Title,
                    Content = card.Content,
                    ListId = card.ListId,
                }).ToList()
            };
        }
    }
}