using System.Threading.Tasks;
using LetsCode.Kanban.Application.Core;
using LetsCode.Kanban.Application.Persistence;
using FluentValidation;
using LetsCode.Kanban.Application.Exceptions;
using System.Linq;

namespace LetsCode.Kanban.Application.UserActions.RemoveCard
{
    public class RemoveCardAction
    {
        private readonly RemoveCardParametersValidator _parametersValidator;
        private readonly IActionContext _ctx;
        private readonly ICardsRepository _cards;

        public RemoveCardAction(
            RemoveCardParametersValidator parametersValidator,
            IActionContext ctx,
            ICardsRepository cards)
        {
            _parametersValidator = parametersValidator;
            _ctx = ctx;
            _cards = cards;
        }

        public async Task<RemoveCardResult> Execute(RemoveCardParameters parameters)
        {
            await _parametersValidator.ValidateAndThrowAsync(parameters);

            var cardToRemove = await _cards.Find(parameters.Id);
            if (cardToRemove == null)
            {
                throw new NotFoundException();
            }

            await _cards.Remove(cardToRemove);

            _ctx.RegisterUserAction(cardToRemove, UserAction.Remove);

            var allCards = await _cards.ListAll();

            return new RemoveCardResult
            {
                Cards = allCards.Select(card => new RemoveCardResult.Card
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