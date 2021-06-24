using System;
using System.Threading.Tasks;
using AutoBogus;
using FluentValidation;
using LetsCode.Kanban.Application.Core;
using LetsCode.Kanban.Application.Persistence;
using LetsCode.Kanban.Application.UserActions.RemoveCard;
using Moq;
using Xunit;

namespace LetsCode.Kanban.UnitTests.Application.UserActions.RemoveCard
{
    public class RemoveCardAction_UnitTests
    {
        private readonly RemoveCardAction _removeCardAction;
        public RemoveCardAction_UnitTests()
        {
            _removeCardAction = new RemoveCardAction(
                parametersValidator: new RemoveCardParametersValidator(),
                ctx: new Mock<IActionContext>().Object,
                cards: new Mock<ICardsRepository>().Object
            );
        }

        [Fact]
        public async Task Must_throw_ValidationException_for_invalid_RemoveCardParameters()
        {
            var invalidParameters = new AutoFaker<RemoveCardParameters>()
                .RuleFor(p => p.Id, Guid.Empty)
                .Generate();

            Func<Task> executeRemoveCardAction = () => _removeCardAction.Execute(invalidParameters);

            await Assert.ThrowsAsync<ValidationException>(executeRemoveCardAction);

        }
    }
}