using System.Collections.Generic;
using System.Security.Claims;
using api.Models;
using api.Services.TokenGenerators;

namespace api.Services
{
    public class AccessTokenGenerator
    {
        private readonly AuthenticationConfiguration _configuration;
        private readonly TokenGenerator _tokenGenerator;

        public AccessTokenGenerator(AuthenticationConfiguration configuration, TokenGenerator tokenGenerator)
        {
            _configuration = configuration;
            _tokenGenerator = tokenGenerator;
        }

        public string Generate(User user)
        {
            var claims = new List<Claim>
            {
                new("id", user.Id.ToString()),
                new(ClaimsIdentity.DefaultNameClaimType, user.Username),
                new(ClaimsIdentity.DefaultRoleClaimType, user.RoleName)
            };

            return _tokenGenerator.Generate(
                _configuration.AccessTokenSecret,
                _configuration.Issuer,
                _configuration.Audience,
                _configuration.AccessTokenExpirationMinutes,
                claims);
        }
    }
}