using System.Collections.Generic;
using System.Security.Claims;

namespace Domain.Interfaces
{
    public interface ITokensGenerator
    {
        string GenerateAccessToken(IEnumerable<Claim> claims);
        string GenerateRefreshToken();
    }
}