using System;
using System.Threading.Tasks;
using AutoBogus;
using FluentValidation;
using LetsCode.Kanban.Application.UserActions.UpdateCard;
using Xunit;

namespace LetsCode.Kanban.UnitTests.Application.UserActions.UpdateCard
{
    public class UpdateCardAction_UnitTests
    {
        private readonly UpdateCardAction _updateCardAction;
        public UpdateCardAction_UnitTests()
        {
            _updateCardAction = new UpdateCardAction(
                parametersValidator: new UpdateCardParametersValidator()
            );
        }

        [Fact]
        public async Task Must_throw_ValidationException_for_invalid_UpdateCardParameters()
        {
            var invalidParameters = new AutoFaker<UpdateCardParameters>().Generate();

            Func<Task> executeUpdateCardAction = () => _updateCardAction.Execute(invalidParameters);

            await Assert.ThrowsAsync<ValidationException>(executeUpdateCardAction);

        }
    }
}