using System.Security.Claims;

namespace RespaunceV2.Core.Interfaces
{
    public interface IJwtTokenValidator
    {
        ClaimsPrincipal GetPrincipalFromToken(string token, string signingKey, bool validateLifetime);
    }
}