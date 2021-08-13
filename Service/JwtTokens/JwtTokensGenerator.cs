using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Service.JwtTokens
{
    public class JwtTokensGenerator : ITokensGenerator
    {
        private readonly AuthenticationConfiguration _configuration;

        public JwtTokensGenerator(IConfiguration configuration)
        {
            _configuration = new AuthenticationConfiguration();
            configuration.Bind("Authentication", _configuration);
        }

        public string GenerateAccessToken(IEnumerable<Claim> claims)
        {
            return GenerateToken(
                _configuration.AccessTokenSecret,
                _configuration.Issuer,
                _configuration.Audience,
                _configuration.AccessTokenExpirationMinutes,
                claims);
        }

        public string GenerateRefreshToken()
        {
            return GenerateToken(
                _configuration.RefreshTokenSecret,
                _configuration.Issuer,
                _configuration.Audience,
                _configuration.RefreshTokenExpirationMinutes);
        }

        private string GenerateToken(string secretKey, string issuer, string audience, double expirationMinutes,
            IEnumerable<Claim> claims = null)
        {
            SecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer,
                audience,
                claims,
                DateTime.UtcNow,
                DateTime.UtcNow.AddMinutes(expirationMinutes),
                credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}