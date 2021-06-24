using AutoBogus;
using FluentValidation.TestHelper;
using LetsCode.Kanban.Application.UserActions.AddCard;
using Xunit;

namespace LetsCode.Kanban.UnitTests.Application.UserActions.AddCard
{
    public class AddCardParametersValidatorTests
    {
        private readonly AddCardParametersValidator _validator;

        public AddCardParametersValidatorTests()
        {
            _validator = new AddCardParametersValidator();
        }

        [Theory]
        [InlineData("t")]
        [InlineData("tttttttttttttttttttttttttttttttttttttttttttttttttt")]
        [InlineData("Título comum")]
        public void Should_handle_Valid_titles(string titleToValidate)
        {
            var parameters = new AutoFaker<AddCardParameters>()
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
            var parameters = new AutoFaker<AddCardParameters>()
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
            var parameters = new AutoFaker<AddCardParameters>()
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
            var parameters = new AutoFaker<AddCardParameters>()
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
            var parameters = new AutoFaker<AddCardParameters>()
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
            var parameters = new AutoFaker<AddCardParameters>()
                .RuleFor(p => p.ListId, listIdToValidate)
                .Generate();

            var result = _validator.TestValidate(parameters);

            result.ShouldHaveValidationErrorFor(p => p.ListId);
        }
    }
}