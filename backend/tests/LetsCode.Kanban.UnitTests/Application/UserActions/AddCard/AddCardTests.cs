using Xunit;
using LetsCode.Kanban.Application.UserActions.AddCard;

namespace LetsCode.Kanban.UnitTests.Application.UserActions.AddCard
{
    public class AddCardTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("    ")]
        [InlineData("title is loooooooooooooooooooooooooooooooooooooooooooooooooooooooong")]
        public void Should_throw_ValidationException_for_invalid_titles(string titleToValidate)
        {
            var addCard = new AddCardAction();
            addCard.Execute()
        }
    }
}