using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using LetsCode.Kanban.Application.Core;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace LetsCode.Kanban.Application.Authentication.Implementation
{
    public class JwtGeneratorOptions
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Secret { private get; set; }
        public TimeSpan? ExpiresAfter { get; set; }

        public SecurityKey GetSigningKey() =>
            new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Secret));
        
    }

    public class JwtGenerator : IJwtGenerator
    {
        private readonly JwtGeneratorOptions _options;
        private readonly IDateTime _dateTime;

        public JwtGenerator(IOptions<JwtGeneratorOptions> options, IDateTime dateTime)
        {
            _options = options.Value;
            _dateTime = dateTime;
        }

        public Task<string> GenerateJwtFor(ClaimsIdentity identity)
        {
            var tokenHandler = new JwtSecurityTokenHandler
            {
                SetDefaultTimesOnTokenCreation = false,
            };
            tokenHandler.OutboundClaimTypeMap[ClaimTypes.NameIdentifier] = "sub";

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identity,
                Issuer = _options.Issuer,
                Audience = _options.Audience,
                IssuedAt = _dateTime.Now,
                SigningCredentials = new SigningCredentials(
                    key: _options.GetSigningKey(), 
                    algorithm: SecurityAlgorithms.HmacSha256Signature)
            };

            if (_options.ExpiresAfter != null)
            {
                tokenDescriptor.Expires = tokenDescriptor.IssuedAt + _options.ExpiresAfter;
            }

            var token = tokenHandler.CreateEncodedJwt(tokenDescriptor);
            return Task.FromResult(token);
        }
    }
}