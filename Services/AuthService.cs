using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using RespaunceV2.Core.Interfaces;
using RespaunceV2.Core.Models;

namespace RespaunceV2.Services
{
    public class AuthService : IAuthService
    {
        private readonly IJwtTokenValidator _jwtTokenValidator;
        private readonly AuthSettings _authSettings;
        private readonly UserManager<ApplicationUser> _userManager;
        public AuthService(
            IJwtTokenValidator jwtTokenValidator,
            IOptions<AuthSettings> authSettings,
            UserManager<ApplicationUser> userManager
        )
        {
            _jwtTokenValidator = jwtTokenValidator;
            _authSettings = authSettings.Value;
            _userManager = userManager;
        }

        public async Task<ApplicationUser> GetUserFromAuthHeader(HttpRequest request)
        {
            var token = request.Headers.SingleOrDefault(h => h.Key == "Authorization").Value.ToString().Replace("bearer ", "");
            var cp = _jwtTokenValidator.GetPrincipalFromToken(token, _authSettings.SigningKey, true);
            var userId = cp.FindFirstValue("id");
            var user  = await _userManager.FindByIdAsync(userId);
            
            return user;
        }
    }
}