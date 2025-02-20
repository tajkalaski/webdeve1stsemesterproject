using System;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using RespaunceV2.Core.Models;
using System.Security.Principal;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using  RespaunceV2.Core.Interfaces;

namespace RespaunceV2.Infrastructure.Auth
{
    public class JwtFactory : IJwtFactory
    {
        private readonly JwtIssuerOptions _jwtOptions;

        public JwtFactory(IOptions<JwtIssuerOptions> jwtOptions)
        {
            _jwtOptions = jwtOptions.Value;
            ThrowIfInvalidOptions(_jwtOptions);
        }

        public async Task<string> GenerateEncodedToken(string id, string email, string role)
        {

            var identity = GenerateClaimsIdentity(id, email, role);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Jti, await _jwtOptions.JtiGenerator()),
                new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(_jwtOptions.IssuedAt).ToString(), ClaimValueTypes.Integer64),
                identity.FindFirst(Helpers.Constants.Strings.JwtClaimIdentifiers.Rol),
                identity.FindFirst(Helpers.Constants.Strings.JwtClaimIdentifiers.Id)
            };

            var jwt = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                notBefore: _jwtOptions.NotBefore,
                expires: _jwtOptions.Expiration,
                signingCredentials: _jwtOptions.SigningCredentials);

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
        }

        public ClaimsIdentity GenerateClaimsIdentity(string id, string email, string role)
        {
            if (role == "Admin")
            {
                return new ClaimsIdentity(new GenericIdentity(email, "Token"), new[]
                {
                    new Claim(Helpers.Constants.Strings.JwtClaimIdentifiers.Id, id),
                    new Claim(Helpers.Constants.Strings.JwtClaimIdentifiers.Rol, Helpers.Constants.Strings.JwtClaims.AdminAccess),
                });
            }

            if (role == "Management")
            {
                return new ClaimsIdentity(new GenericIdentity(email, "Token"), new[]
                {
                    new Claim(Helpers.Constants.Strings.JwtClaimIdentifiers.Id, id),
                    new Claim(Helpers.Constants.Strings.JwtClaimIdentifiers.Rol, Helpers.Constants.Strings.JwtClaims.ManagementAccess),
                });
            }

            if (role == "RegularUser")
            {
                return new ClaimsIdentity(new GenericIdentity(email, "Token"), new[]
                {
                    new Claim(Helpers.Constants.Strings.JwtClaimIdentifiers.Id, id),
                    new Claim(Helpers.Constants.Strings.JwtClaimIdentifiers.Rol, Helpers.Constants.Strings.JwtClaims.RegularUserAccess),
                });
            }

            if (role == "Supplier")
            {
                return new ClaimsIdentity(new GenericIdentity(email, "Token"), new[]
                {
                    new Claim(Helpers.Constants.Strings.JwtClaimIdentifiers.Id, id),
                    new Claim(Helpers.Constants.Strings.JwtClaimIdentifiers.Rol, Helpers.Constants.Strings.JwtClaims.SupplierUserAccess),
                });
            }

            return new ClaimsIdentity(new GenericIdentity(email, "Token"), new[]
            {
                new Claim(Helpers.Constants.Strings.JwtClaimIdentifiers.Id, id),
                new Claim(Helpers.Constants.Strings.JwtClaimIdentifiers.Rol, Helpers.Constants.Strings.JwtClaims.ApiAccess),
            });
        }

        /// <returns>Date converted to seconds since Unix epoch (Jan 1, 1970, midnight UTC).</returns>
        private static long ToUnixEpochDate(DateTime date)
          => (long)Math.Round((date.ToUniversalTime() -
                               new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero))
                              .TotalSeconds);

        private static void ThrowIfInvalidOptions(JwtIssuerOptions options)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));

            if (options.ValidFor <= TimeSpan.Zero)
            {
                throw new ArgumentException("Must be a non-zero TimeSpan.", nameof(JwtIssuerOptions.ValidFor));
            }

            if (options.SigningCredentials == null)
            {
                throw new ArgumentNullException(nameof(JwtIssuerOptions.SigningCredentials));
            }

            if (options.JtiGenerator == null)
            {
                throw new ArgumentNullException(nameof(JwtIssuerOptions.JtiGenerator));
            }
        }
    }
}