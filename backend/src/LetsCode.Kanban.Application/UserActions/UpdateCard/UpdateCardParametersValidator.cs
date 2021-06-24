using FluentValidation;

namespace LetsCode.Kanban.Application.UserActions.UpdateCard
{
    public class UpdateCardParametersValidator : AbstractValidator<UpdateCardParameters>
    {
        public UpdateCardParametersValidator()
        {
            RuleFor(p => p.Id)
                .NotEmpty();

            RuleFor(p => p.Title)
                .NotEmpty()
                .Length(1, 50);

            RuleFor(p => p.Content)
                .NotEmpty();

            RuleFor(p => p.ListId)
                .NotEmpty()
                .In("ToDo", "Doing", "Done");
        }
    }
}