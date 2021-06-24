using System.Threading.Tasks;
using FluentValidation;
using LetsCode.Kanban.Application.Core;

namespace LetsCode.Kanban.Application.UserActions.AddCard
{
    public class AddCardAction
    {
        private readonly IActionContext _ctx;
        private readonly AddCardParametersValidator _parametersValidator;

        public AddCardAction(
            AddCardParametersValidator parametersValidator,
            IActionContext ctx)
        {
            _parametersValidator = parametersValidator;
            _ctx = ctx;
        }

        public async Task Execute(AddCardParameters parameters)
        {
            var validationResult = await _parametersValidator.ValidateAsync(parameters, _ctx.Cancel);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }
        }
    }
}