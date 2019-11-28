using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RespaunceV2.Core.Interfaces;
using RespaunceV2.Core.Models;
using RespaunceV2.WebApi.Resources;

namespace RespaunceV2.Controllers
{
    [Route("api/users")]
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly IUnitOfWork _unitOfWork;

        public UserController(
            UserManager<ApplicationUser> userManager,
            IMapper mapper,
            IEmailService emailService,
            IUnitOfWork unitOfWork

        )
        {
            _userManager = userManager;
            _mapper = mapper;
            _emailService = emailService;
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        [Authorize(Policy = "AdminOrSupplier")]
        public async Task<IActionResult> Create([FromBody] UserResource userResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (userResource.Language == null) userResource.Language = "7cef445e-7fae-4514-b7fa-624982cfc130";
            var createIdentity = _mapper.Map<ApplicationUser>(userResource);
            createIdentity.UserName = createIdentity.Email;

            var userPassword = GenerateRandomPassword();
            var result = await _userManager.CreateAsync(createIdentity, userPassword);

            if (!result.Succeeded) return BadRequest(result);

            var userIdentity = await _userManager.Users
                .Include(u => u.Role)
                .Include(u => u.Language)
                .Include(u => u.Company)
                .SingleOrDefaultAsync(u => u.NormalizedEmail == userResource.Email.ToUpper());

            await _emailService.SendAccessGrantedMail(userIdentity.FirstName + " " + userIdentity.LastName, userIdentity.Email, userPassword, userIdentity.Company.Name);

            var newUserResource = _mapper.Map<UserResource>(userIdentity);

            return Ok(newUserResource);
        }

        [HttpGet("{email}")]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> GetByEmail(string email)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.Users
                .Include(u => u.Role)
                .Include(u => u.Language)
                .Include(u => u.Company)
                .SingleOrDefaultAsync(u => u.NormalizedEmail == email.ToUpper());

            if (user == null)
                return NotFound();

            var userResource = _mapper.Map<UserResource>(user);

            return Ok(userResource);
        }

        [HttpGet]
        [Authorize(Policy = "AdminOrSupplier")]
        public async Task<IActionResult> GetByCompany(string companyId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var users = await _userManager.Users
                .Where(u => u.Company.Id == companyId)
                .Include(u => u.Role)
                .Include(u => u.Language)
                .Include(u => u.Company)
                .ToListAsync();

            if (users == null)
                return NotFound();

            var usersResource = _mapper.Map<List<UserResource>>(users);

            return Ok(usersResource);
        }

        [HttpPut]
        [Authorize(Policy="Admin")]
        public async Task<IActionResult> Update([FromBody] UserResource userResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.FindByEmailAsync(userResource.Email);

            if (user == null)
                return NotFound();

            _mapper.Map<UserResource, ApplicationUser>(userResource, user);

            await _unitOfWork.CompleteAsync();

            user = await _userManager.Users
                .Include(u => u.Role)
                .Include(u => u.Language)
                .Include(u => u.Company)
                .SingleOrDefaultAsync(u => u.NormalizedEmail == userResource.Email.ToUpper());

            var updatedUserResource = _mapper.Map<ApplicationUser, UserResource>(user, userResource);

            return Ok(updatedUserResource);
        }

        [HttpDelete("{email}")]
        [Authorize(Policy="Admin")]
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