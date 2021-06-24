using FluentValidation;
using LetsCode.Kanban.Application.Models;

namespace LetsCode.Kanban.Application.UserActions.AddCard
{
    public class AddCardParametersValidator : AbstractValidator<AddCardParameters>
    {
        public AddCardParametersValidator()
        {
            RuleFor(p => p.Title)
                .NotEmpty()
                .Length(1, 50);

            RuleFor(p => p.Content)
                .NotEmpty();

            RuleFor(p => p.ListId)
                .NotEmpty()
                .In(BoardList.Ids);
        }
    }
}