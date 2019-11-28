using System;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using RespaunceV2.Core.Models;
using  RespaunceV2.Core.Interfaces;

namespace RespaunceV2.Infrastructure.Auth
{
    public class JwtTokenValidator : IJwtTokenValidator
    {
        private readonly IJwtTokenHandler _jwtTokenHandler;

        public JwtTokenValidator(IJwtTokenHandler jwtTokenHandler)
        {
            _jwtTokenHandler = jwtTokenHandler;
        }

        public ClaimsPrincipal GetPrincipalFromToken(string token, string signingKey, bool validateLifetime)
        {
            return _jwtTokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateAudience = true,
                ValidAudience = "https://reporting.respaunce.com",

                ValidateIssuer = true,
                ValidIssuer = "Respaunce",

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey)),
                ValidateLifetime = validateLifetime,
                ClockSkew = TimeSpan.Zero
            });
        }
    }
}