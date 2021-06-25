using FluentValidation;

namespace LetsCode.Kanban.Application.UserActions.Login
{
    public class LoginParametersValidator : AbstractValidator<LoginParameters>
    {
        public LoginParametersValidator()
        {
            RuleFor(p => p.Username)
                .NotEmpty();

            RuleFor(p => p.Password)
                .NotNull()
                .MinimumLength(1);
        }
    }
}