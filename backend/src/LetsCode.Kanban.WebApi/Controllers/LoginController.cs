using System.Threading.Tasks;
using LetsCode.Kanban.Application.UserActions.Login;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LetsCode.Kanban.WebApi.Controllers
{
    [ApiController]
    [Route("login")]
    public class LoginController : ControllerBase
    {
        [HttpPost("")]
        public async Task<LoginResult> Login(LoginParameters parameters,
            [FromServices] LoginAction loginAction)
        {
            var result = await loginAction.Execute(parameters);

            Response.Cookies.Append("AuthJwt", result.Jwt, new CookieOptions()
            {
                HttpOnly = true,
            });

            return result;
        }

        [Authorize]
        [HttpGet("test")]
        public string TestLogin()
        {
            var n = User;
            return "OK";
        }
    }
}