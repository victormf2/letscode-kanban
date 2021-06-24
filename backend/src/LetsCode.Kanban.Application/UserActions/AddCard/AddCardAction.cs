using FluentValidation;
using LetsCode.Kanban.Application.Core;
using LetsCode.Kanban.Application.Models;
using LetsCode.Kanban.Application.Persistence;
using System.Threading.Tasks;

namespace LetsCode.Kanban.Application.UserActions.AddCard
{
    public class AddCardAction
    {
        private readonly IActionContext _ctx;
        private readonly AddCardParametersValidator _parametersValidator;
        private readonly ICardsRepository _cards;

        public AddCardAction(
            AddCardParametersValidator parametersValidator,
            IActionContext ctx,
            ICardsRepository cards)
        {
            _parametersValidator = parametersValidator;
            _ctx = ctx;
            _cards = cards;
        }

        public async Task<AddCardResult> Execute(AddCardParameters parameters)
        {
            await _parametersValidator.ValidateAndThrowAsync(parameters, _ctx.Cancel);

            var card = new Card
            {
                Title = parameters.Title,
                Content = parameters.Content,
                ListId = parameters.ListId,
            };

            var generatedCardId = await _cards.Add(card);

            return new AddCardResult
            {
                Id = generatedCardId,
                Title = card.Title,
                Content = card.Content,
                ListId = card.ListId,
            };
        }
    }
}