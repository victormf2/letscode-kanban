using System;
using System.Threading.Tasks;
using AutoBogus;
using FluentValidation;
using LetsCode.Kanban.Application.Core;
using LetsCode.Kanban.Application.Persistence;
using LetsCode.Kanban.Application.UserActions.AddCard;
using Moq;
using Xunit;

namespace LetsCode.Kanban.UnitTests.Application.UserActions.AddCard
{
    public class AddCardAction_UnitTests
    {
        private readonly AddCardAction _addCardAction;
        public AddCardAction_UnitTests()
        {
            _addCardAction = new AddCardAction(
                parametersValidator: new AddCardParametersValidator(),
                ctx: new Mock<IActionContext>().Object,
                cards: new Mock<ICardsRepository>().Object
            );
        }

        [Fact]
        public async Task Must_throw_ValidationException_for_invalid_AddCardParameters()
        {
            var invalidParameters = new AutoFaker<AddCardParameters>().Generate();

            Func<Task> addCardActionExecute = () => _addCardAction.Execute(invalidParameters);

            await Assert.ThrowsAsync<ValidationException>(addCardActionExecute);
        }
    }
}