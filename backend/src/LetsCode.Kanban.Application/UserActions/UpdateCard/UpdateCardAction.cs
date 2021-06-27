using System.Threading.Tasks;
using FluentValidation;
using LetsCode.Kanban.Application.Core;
using LetsCode.Kanban.Application.Exceptions;
using LetsCode.Kanban.Application.Persistence;

namespace LetsCode.Kanban.Application.UserActions.UpdateCard
{
    public class UpdateCardAction
    {
        private readonly UpdateCardParametersValidator _parametersValidator;
        private readonly IActionContext _ctx;
        private readonly ICardsRepository _cards;

        public UpdateCardAction(
            UpdateCardParametersValidator parametersValidator,
            IActionContext ctx,
            ICardsRepository cards)
        {
            _parametersValidator = parametersValidator;
            _ctx = ctx;
            _cards = cards;
        }

        public async Task<UpdateCardResult> Execute(UpdateCardParameters parameters)
        {
            await _parametersValidator.ValidateAndThrowAsync(parameters, _ctx.Cancel);

            var card = await _cards.Find(parameters.Id);
            if (card == null)
            {
                throw new NotFoundException();
            }

            card.Title = parameters.Title;
            card.Content = parameters.Content;
            card.ListId = parameters.ListId;

            await _cards.Update(card);

            _ctx.RegisterUserAction(card, UserAction.Update);

            return new UpdateCardResult
            {
                Id = card.Id,
                Title = card.Title,
                Content = card.Content,
                ListId = card.ListId,
            };
        }
    }
}