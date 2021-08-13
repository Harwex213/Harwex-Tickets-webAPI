using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Service.JwtTokens
{
    public class JwtRefreshTokenValidator : IRefreshTokenValidator
    {
        private readonly AuthenticationConfiguration _configuration;

        public JwtRefreshTokenValidator(IConfiguration configuration)
        {
            _configuration = new AuthenticationConfiguration();
            configuration.Bind("Authentication", _configuration);
        }

        public bool Validate(string refreshToken)
        {
            var validationParameters = new TokenValidationParameters
            {
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.RefreshTokenSecret)),
                ValidIssuer = _configuration.Issuer,
                ValidAudience = _configuration.Audience,
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                tokenHandler.ValidateToken(refreshToken, validationParameters, out _);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}