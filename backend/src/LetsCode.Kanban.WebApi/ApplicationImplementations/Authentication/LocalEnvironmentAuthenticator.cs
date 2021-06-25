using System.Security.Claims;
using System.Threading.Tasks;
using LetsCode.Kanban.Application.Authentication;
using Microsoft.Extensions.Options;

namespace LetsCode.Kanban.WebApi.ApplicationImplementations.Authentication
{
    public class LocalEnvironmentAuthenticatorOptions
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
    public class LocalEnvironmentAuthenticator : IAuthenticator
    {
        private readonly LocalEnvironmentAuthenticatorOptions _options;
        public LocalEnvironmentAuthenticator(IOptions<LocalEnvironmentAuthenticatorOptions> options)
        {
            _options = options.Value;
        }

        public Task<ClaimsIdentity> Authenticate(string username, string password)
        {
            var credentialsAreValid = username == _options.Username && password == _options.Password;
            if (!credentialsAreValid)
            {
                return Task.FromResult((ClaimsIdentity)null);
            }

            var identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, username)
            });

            return Task.FromResult(identity);
        }
    }
}