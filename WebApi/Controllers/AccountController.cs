using System;
using System.Collections.Generic;
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
    [Route("api/accounts")]
    public class AccountController : Controller
    {
        private readonly IJwtTokenValidator _jwtTokenValidator;
        private readonly AuthSettings _authSettings;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;

        public AccountController(
            IJwtTokenValidator jwtTokenValidator,
            IOptions<AuthSettings> authSettings,
            UserManager<ApplicationUser> userManager,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IEmailService emailService
        )
        {
            _jwtTokenValidator = jwtTokenValidator;
            _authSettings = authSettings.Value;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _emailService = emailService;
        }


        [HttpPost]
        //[Authorize(Policy = "Admin")]
        [AllowAnonymous]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] UserResource userResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userIdentity = _mapper.Map<ApplicationUser>(userResource);
            userIdentity.UserName = userIdentity.Email;

            var userPassword = GenerateRandomPassword();
            var result = await _userManager.CreateAsync(userIdentity, userPassword);

            if (!result.Succeeded) return BadRequest(result);

            await _emailService.SendAccessGrantedMail(userIdentity.FirstName + " " + userIdentity.LastName, userIdentity.Email, userPassword, userResource.CompanyName);

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetByHeader()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var token = Request.Headers.SingleOrDefault(h => h.Key == "Authorization").Value.ToString().Replace("bearer ", "");

            var cp = _jwtTokenValidator.GetPrincipalFromToken(token, _authSettings.SigningKey, true);

            var id = cp.FindFirstValue("id");

            var identityUser = await _userManager.Users
                .Include(u => u.Role)
                .Include(u => u.Language)
                .Include(u => u.Company)
                .SingleOrDefaultAsync(u => u.Id == id);

            var userResource = _mapper.Map<UserResource>(identityUser);

            return Ok(userResource);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateProfileResource userResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.FindByEmailAsync(userResource.Email);

            if (user == null)
                return NotFound();

            _mapper.Map<UpdateProfileResource, ApplicationUser>(userResource, user);

            await _unitOfWork.CompleteAsync();

            user = await _userManager.Users
                .Include(u => u.Role)
                .Include(u => u.Language)
                .SingleOrDefaultAsync(u => u.NormalizedEmail == userResource.Email.ToUpper());

            var updatedUserResource = _mapper.Map<UserResource>(userResource);
            updatedUserResource = _mapper.Map<ApplicationUser, UserResource>(user, updatedUserResource);

            return Ok(updatedUserResource);
        }

        [HttpDelete("{email}")]
        public async Task<IActionResult> Delete(string email)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
                return NotFound();

            if (user.RoleId == "82630b73-c4d4-48a5-b1c8-aab8002f4bca")
                return BadRequest();

            await _userManager.DeleteAsync(user);
            await _unitOfWork.CompleteAsync();

            return Ok();
        }

        [HttpPost]
        [Route("changePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordResource changePasswordResource, UserResource userResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userIdentity = _mapper.Map<ApplicationUser>(userResource);

            var result = await _userManager.ChangePasswordAsync(userIdentity, changePasswordResource.CurrentPassword, changePasswordResource.NewPassword);
            if (!result.Succeeded) return BadRequest(result.Errors);

            return Ok();
        }

        [HttpGet()]
        [AllowAnonymous]
        [Route("resetPassword")]
        public async Task<IActionResult> ResetPassword(string email)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userIdentity = await _userManager.FindByEmailAsync(email.ToUpper());
            userIdentity.UserName = userIdentity.Email;

            var newPassword = GenerateRandomPassword();
            var passwordResetToken = await _userManager.GeneratePasswordResetTokenAsync(userIdentity);
            var result = await _userManager.ResetPasswordAsync(userIdentity, passwordResetToken, newPassword);

            if (!result.Succeeded) return BadRequest(result);
 
            var userResource = _mapper.Map<UserResource>(userIdentity);

            await _emailService.SendAccessGrantedMail(userResource.FirstName + " " + userResource.LastName, userResource.Email, newPassword, userResource.CompanyName);

            await _unitOfWork.CompleteAsync();
            return Ok();
        }

        public static string GenerateRandomPassword(PasswordOptions opts = null)
        {
            if (opts == null) opts = new PasswordOptions()
            {
                RequiredLength = 10,
                RequiredUniqueChars = 4,
                RequireDigit = true,
                RequireLowercase = true,
                RequireNonAlphanumeric = true,
                RequireUppercase = true
            };

            string[] randomChars = new[] {
                "ABCDEFGHJKLMNOPQRSTUVWXYZ",    // uppercase 
                "abcdefghijkmnopqrstuvwxyz",    // lowercase
                "0123456789",                   // digits
                "!@$?_-"                        // non-alphanumeric
            };
            Random rand = new Random(Environment.TickCount);
            List<char> chars = new List<char>();

            if (opts.RequireUppercase)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[0][rand.Next(0, randomChars[0].Length)]);

            if (opts.RequireLowercase)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[1][rand.Next(0, randomChars[1].Length)]);

            if (opts.RequireDigit)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[2][rand.Next(0, randomChars[2].Length)]);

            if (opts.RequireNonAlphanumeric)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[3][rand.Next(0, randomChars[3].Length)]);

            for (int i = chars.Count; i < opts.RequiredLength
                || chars.Distinct().Count() < opts.RequiredUniqueChars; i++)
            {
                string rcs = randomChars[rand.Next(0, randomChars.Length)];
                chars.Insert(rand.Next(0, chars.Count),
                    rcs[rand.Next(0, rcs.Length)]);
            }

            return new string(chars.ToArray());
        }
    }
}