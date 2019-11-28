using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RespaunceV2.Core.Interfaces;
using RespaunceV2.Core.Models;
using RespaunceV2.WebApi.Resources;

namespace RespaunceV2.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly AuthSettings _authSettings;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokenFactory _tokenFactory;
        private readonly IJwtFactory _jwtFactory;
        private readonly IJwtTokenValidator _jwtTokenValidator;
        private readonly JwtIssuerOptions _jwtOptions;
        private readonly IMapper _mapper;

        // TO DO: Clean up the mess in the constructor
        public AuthController(
            UserManager<ApplicationUser> userManager,
            ITokenFactory tokenFactory,
            IJwtFactory jwtFactory,
            IJwtTokenValidator jwtTokenValidator,
            IOptions<JwtIssuerOptions> jwtOptions,
            IOptions<AuthSettings> authSettings,
            IMapper mapper,
            IRefreshTokenRepository refreshTokenRepository,
            IUnitOfWork unitOfWork
            )
        {
            _userManager = userManager;
            _jwtFactory = jwtFactory;
            _tokenFactory = tokenFactory;
            _jwtTokenValidator = jwtTokenValidator;
            _jwtOptions = jwtOptions.Value;
            _authSettings = authSettings.Value;
            _mapper = mapper;
            _refreshTokenRepository = refreshTokenRepository;
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginFormResource credentials)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.Users.Include(u => u.Role).SingleOrDefaultAsync(u => u.NormalizedEmail == credentials.Email.ToUpper());

            if (!await _userManager.CheckPasswordAsync(user, credentials.Password))
            {
                return Unauthorized();
            }

            var identity = await GetClaimsIdentity(user);

            if (identity == null)
            {
                return BadRequest(ModelState);
            }

            var existingRefreshToken = await _refreshTokenRepository.GetByUserId(user.Id);

            if (existingRefreshToken == null)
            {
                var refreshToken = await _refreshTokenRepository.CreateToken(user.Id, "");
                await _unitOfWork.CompleteAsync();

                // TO DO: Create a response class with appropriate constructor
                var response = new
                {
                    token = await _jwtFactory.GenerateEncodedToken(user.Id, credentials.Email, user.Role.Name),
                    refreshToken = refreshToken,
                    expires_in = (int)_jwtOptions.ValidFor.TotalSeconds
                };
                return Ok(response);
            }
            else
            {
                _refreshTokenRepository.Delete(existingRefreshToken);
                var token = await _refreshTokenRepository.CreateToken(user.Id, "");
                await _unitOfWork.CompleteAsync();

                // TO DO: Create a response class with appropriate constructor
                var response = new
                {
                    token = await _jwtFactory.GenerateEncodedToken(user.Id, credentials.Email, user.Role.Name),
                    refreshToken = token,
                    expires_in = (int)_jwtOptions.ValidFor.TotalSeconds
                };
                return Ok(response);
            }
        }

        private async Task<ClaimsIdentity> GetClaimsIdentity(ApplicationUser userToVerify)
        {
            if (userToVerify != null)
            {
                return await Task.FromResult(_jwtFactory.GenerateClaimsIdentity(userToVerify.Id, userToVerify.Email, userToVerify.Role.Name));
            }
            return await Task.FromResult<ClaimsIdentity>(null);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("refreshToken")]
        public async Task<IActionResult> RefreshToken([FromBody] CheckTokenResource checkTokenResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var cp = _jwtTokenValidator.GetPrincipalFromToken(checkTokenResource.Token, _authSettings.SigningKey, false);

            if (cp != null)
            {
                var id = cp.Claims.Single(c => c.Type == "id");
                var user = await _userManager.Users.Include(u => u.Role).SingleOrDefaultAsync(u => u.Id == id.Value);
                var existingRefreshToken = await _refreshTokenRepository.GetByUserId(user.Id);

                if (existingRefreshToken != null)
                {
                    var refreshToken = _refreshTokenRepository.CreateToken(user.Id, "");
                    _refreshTokenRepository.Delete(existingRefreshToken);
                    await _unitOfWork.CompleteAsync();

                    // TO DO: Create a response class with appropriate constructor
                    var response = new
                    {
                        token = await _jwtFactory.GenerateEncodedToken(user.Id, user.Email, user.Role.Name),
                        refreshToken = refreshToken.Result,
                        expires_in = (int)_jwtOptions.ValidFor.TotalSeconds
                    };
                    return Ok(response);
                }
            }
            return BadRequest(ModelState);
        }
    }
}