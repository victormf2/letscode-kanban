using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;
using LetsCode.Kanban.Application.Core;
using System;
using LetsCode.Kanban.Application.Authentication.Implementation;

namespace LetsCode.Kanban.UnitTests.WebApi.ApplicationImplementations.Authentication
{
    public class JwtGenerator_UnitTests
    {
        [Fact]
        public async Task Must_generate_correct_jwt()
        {
            var options = new Mock<IOptions<JwtGeneratorOptions>>();
            options.SetupGet(o => o.Value).Returns(new JwtGeneratorOptions()
            {
                Issuer = "test_issuer",
                Audience = "localhost",
                ExpiresAfter = TimeSpan.FromMinutes(15),
                Secret = "zEc2bC2Y1cnqkOgXRVw2E9nweF7XIoB5"
            });

            var dateTime = new Mock<IDateTime>();
            dateTime.SetupGet(d => d.Now).Returns(new DateTime(2020, 12, 3));
            
            var jwtGenerator = new JwtGenerator(options.Object, dateTime.Object);

            var identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, "victor"),
            });

            var actualJwt = await jwtGenerator.GenerateJwtFor(identity);

            var expectedJwt = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJ2aWN0b3IiLCJleHAiOjE2MDY5NjUzMDAsImlhdCI6MTYwNjk2NDQwMCwiaXNzIjoidGVzdF9pc3N1ZXIiLCJhdWQiOiJsb2NhbGhvc3QifQ.oQliC8irnvkPmh0PtBF0iiQC1jyqWw4WUdWkvcKvyIc";
            Assert.Equal(expectedJwt, actualJwt);
        }
    }
}