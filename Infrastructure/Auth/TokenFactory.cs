using System;
using System.Security.Cryptography;
using RespaunceV2.Core.Interfaces;

namespace RespaunceV2.Infrastructure.Auth
{
    public class TokenFactory : ITokenFactory
    {
        public string GenerateToken(int size = 32)
        {
            var randomNumber = new byte[size];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
    }
}