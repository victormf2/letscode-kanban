using FluentValidation;

namespace LetsCode.Kanban.Application.UserActions.RemoveCard
{
    public class RemoveCardParametersValidator : AbstractValidator<RemoveCardParameters>
    {
        public RemoveCardParametersValidator()
        {
            RuleFor(p => p.Id)
                .NotEmpty();
        }
    }
}