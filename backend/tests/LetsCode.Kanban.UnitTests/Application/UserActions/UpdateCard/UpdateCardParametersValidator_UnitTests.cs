using AutoBogus;
using FluentValidation.TestHelper;
using LetsCode.Kanban.Application.UserActions.UpdateCard;
using System;
using Xunit;

namespace LetsCode.Kanban.UnitTests.Application.UserActions.UpdateCard
{
    public class UpdateCardParametersValidator_UnitTests
    {
        private readonly UpdateCardParametersValidator _validator;

        public UpdateCardParametersValidator_UnitTests()
        {
            _validator = new UpdateCardParametersValidator();
        }

        [Theory]
        [InlineData("1a984abd-e779-4d8b-a97e-1a167ee57a9c")]
        public void Should_handle_Valid_card_ids(string cardIdToValidate)
        {
            var parameters = new AutoFaker<UpdateCardParameters>()
                .RuleFor(p => p.Id, Guid.Parse(cardIdToValidate))
                .Generate();

            var result = _validator.TestValidate(parameters);

            result.ShouldNotHaveValidationErrorFor(p => p.Id);
        }

        [Theory]
        [InlineData("00000000-0000-0000-0000-000000000000")]
        public void Should_handle_Invalid_card_ids(string cardIdToValidate)
        {
            var parameters = new AutoFaker<UpdateCardParameters>()
                .RuleFor(p => p.Id, Guid.Parse(cardIdToValidate))
                .Generate();

            var result = _validator.TestValidate(parameters);

            result.ShouldHaveValidationErrorFor(p => p.Id);
        }


        [Theory]
        [InlineData("t")]
        [InlineData("tttttttttttttttttttttttttttttttttttttttttttttttttt")]
        [InlineData("Título comum")]
        public void Should_handle_Valid_titles(string titleToValidate)
        {
            var parameters = new AutoFaker<UpdateCardParameters>()
                .RuleFor(p => p.Title, titleToValidate)
                .Generate();

            var result = _validator.TestValidate(parameters);

            result.ShouldNotHaveValidationErrorFor(p => p.Title);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("    ")]
        [InlineData("title is loooooooooooooooooooooooooooooooooooooooooooooooooooooooong")]
        public void Should_handle_Invalid_titles(string titleToValidate)
        {
            var parameters = new AutoFaker<UpdateCardParameters>()
                .RuleFor(p => p.Title, titleToValidate)
                .Generate();

            var result = _validator.TestValidate(parameters);

            result.ShouldHaveValidationErrorFor(p => p.Title);
        }

        [Theory]
        [InlineData("Just a content")]
        [InlineData("```\ncódigo em markdown\n```")]
        public void Should_handle_Valid_contents(string contentToValidate)
        {
            var parameters = new AutoFaker<UpdateCardParameters>()
                .RuleFor(p => p.Content, contentToValidate)
                .Generate();

            var result = _validator.TestValidate(parameters);

            result.ShouldNotHaveValidationErrorFor(p => p.Content);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void Should_handle_Invalid_contents(string contentToValidate)
        {
            var parameters = new AutoFaker<UpdateCardParameters>()
                .RuleFor(p => p.Content, contentToValidate)
                .Generate();

            var result = _validator.TestValidate(parameters);

            result.ShouldHaveValidationErrorFor(p => p.Content);
        }

        [Theory]
        [InlineData("ToDo")]
        [InlineData("Doing")]
        [InlineData("Done")]
        public void Should_handle_Valid_list_ids(string listIdToValidate)
        {
            var parameters = new AutoFaker<UpdateCardParameters>()
                .RuleFor(p => p.ListId, listIdToValidate)
                .Generate();

            var result = _validator.TestValidate(parameters);

            result.ShouldNotHaveValidationErrorFor(p => p.ListId);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData("something")]
        public void Should_handle_Invalid_list_ids(string listIdToValidate)
        {
            var parameters = new AutoFaker<UpdateCardParameters>()
                .RuleFor(p => p.ListId, listIdToValidate)
                .Generate();

            var result = _validator.TestValidate(parameters);

            result.ShouldHaveValidationErrorFor(p => p.ListId);
        }
    }
}