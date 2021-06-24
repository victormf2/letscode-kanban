using System;
using AutoBogus;
using FluentValidation.TestHelper;
using LetsCode.Kanban.Application.UserActions.RemoveCard;
using Xunit;

namespace LetsCode.Kanban.UnitTests.Application.UserActions.RemoveCard
{
    public class RemoveCardParametersValidator_UnitTests
    {
        private readonly RemoveCardParametersValidator _validator;

        public RemoveCardParametersValidator_UnitTests()
        {
            _validator = new RemoveCardParametersValidator();
        }

        [Theory]
        [InlineData("1a984abd-e779-4d8b-a97e-1a167ee57a9c")]
        public void Should_handle_Valid_card_ids(string cardIdToValidate)
        {
            var parameters = new AutoFaker<RemoveCardParameters>()
                .RuleFor(p => p.Id, Guid.Parse(cardIdToValidate))
                .Generate();

            var result = _validator.TestValidate(parameters);

            result.ShouldNotHaveValidationErrorFor(p => p.Id);
        }

        [Theory]
        [InlineData("00000000-0000-0000-0000-000000000000")]
        public void Should_handle_Invalid_card_ids(string cardIdToValidate)
        {
            var parameters = new AutoFaker<RemoveCardParameters>()
                .RuleFor(p => p.Id, Guid.Parse(cardIdToValidate))
                .Generate();

            var result = _validator.TestValidate(parameters);

            result.ShouldHaveValidationErrorFor(p => p.Id);
        }
    }
}