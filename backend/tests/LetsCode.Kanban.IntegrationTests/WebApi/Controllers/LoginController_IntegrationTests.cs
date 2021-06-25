using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using LetsCode.Kanban.Application.UserActions.Login;
using Xunit;

namespace LetsCode.Kanban.IntegrationTests.WebApi.Controllers
{
    public class LoginController_IntegrationTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _httpClient;
        private readonly CustomWebApplicationFactory _factory;
        public LoginController_IntegrationTests(CustomWebApplicationFactory factory)
        {
            _factory = factory;
            _httpClient = _factory.CreateClient();
        }

        [Fact]
        public async Task Must_authenticate_on_test_route()
        {
            await Login();

            var response = await _httpClient.GetAsync("/login/test");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        private async Task<LoginResult> Login()
        {
            var parameters = new LoginParameters
            {
                Username = "victor",
                Password = "123456",
            };

            var response = await _httpClient.PostAsJsonAsync("/login", parameters);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var loginResult = await response.Content.ReadFromJsonAsync<LoginResult>();
            return loginResult;
        }
    }
}