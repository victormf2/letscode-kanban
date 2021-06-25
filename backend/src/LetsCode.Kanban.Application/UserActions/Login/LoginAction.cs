using LetsCode.Kanban.Application.Authentication;
using System.Security.Authentication;
using System.Threading.Tasks;

namespace LetsCode.Kanban.Application.UserActions.Login
{
    public class LoginAction
    {
        private readonly IAuthenticator _authenticator;
        private readonly IJwtGenerator _jwtGenerator;

        public LoginAction(IAuthenticator authenticator, IJwtGenerator jwtGenerator)
        {
            _authenticator = authenticator;
            _jwtGenerator = jwtGenerator;
        }

        public async Task<LoginResult> Execute(LoginParameters parameters)
        {
            var identity = await _authenticator.Authenticate(parameters.Username, parameters.Password);
            if (identity == null)
            {
                throw new InvalidCredentialException();
            }

            var jwt = await _jwtGenerator.GenerateJwtFor(identity);
            return new LoginResult
            {
                Jwt = jwt
            };
        }
    }
}