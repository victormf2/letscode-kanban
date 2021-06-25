using System.Security.Claims;
using System.Threading.Tasks;
using LetsCode.Kanban.WebApi.ApplicationImplementations.Authentication;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace LetsCode.Kanban.UnitTests.WebApi.ApplicationImplementations.Authentication
{
    public class LocalEnvironmentAuthenticator_UnitTests
    {
        private readonly LocalEnvironmentAuthenticator _authenticator;
        public LocalEnvironmentAuthenticator_UnitTests()
        {
            var options = new Mock<IOptions<LocalEnvironmentAuthenticatorOptions>>();
            options.SetupGet(o => o.Value).Returns(new LocalEnvironmentAuthenticatorOptions
            {
                Username = "victor",
                Password = "123456"
            });
            _authenticator = new LocalEnvironmentAuthenticator(options.Object);
        }

        [Fact]
        public async Task Must_return_null_for_invalid_credentials()
        {
            var identity = await _authenticator.Authenticate("victor", "password");
            
            Assert.Null(identity);
        }

        [Fact]
        public async Task Must_return_identity_for_valida_credentials()
        {
            var identity = await _authenticator.Authenticate("victor", "123456");

            Assert.NotNull(identity);
            Assert.Contains(identity.Claims, c => c.Type == ClaimTypes.NameIdentifier && c.Value == "victor");
        }
    }
}