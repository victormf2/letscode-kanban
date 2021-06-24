using System.Threading.Tasks;
using FluentValidation;

namespace LetsCode.Kanban.Application.UserActions.UpdateCard
{
    public class UpdateCardAction
    {
        private readonly UpdateCardParametersValidator _parametersValidator;

        public UpdateCardAction(
            UpdateCardParametersValidator parametersValidator)
        {
            _parametersValidator = parametersValidator;
        }

        public async Task Execute(UpdateCardParameters parameters)
        {
            await _parametersValidator.ValidateAndThrowAsync(parameters);
        }
    }
}