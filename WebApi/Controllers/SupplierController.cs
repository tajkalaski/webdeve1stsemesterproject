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
using RespaunceV2.Infrastructure.Persistence;
using RespaunceV2.WebApi.Resources;

namespace RespaunceV2.Controllers
{
    [Route("api/suppliers")]
    public class SupplierController : Controller
    {
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailService _emailService;
        private readonly ICompanyRepository _companyRepository;
        private readonly ISupplierRepository _supplierRepository;
        private readonly IUnitOfWork _unitOfWork;

        public SupplierController(
            IMapper mapper,
            UserManager<ApplicationUser> userManager,
            ICompanyRepository companyRepository,
            IEmailService emailService,
            ISupplierRepository supplierRepository,
            IUnitOfWork unitOfWork
        )
        {
            _mapper = mapper;
            _companyRepository = companyRepository;
            _supplierRepository = supplierRepository;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _emailService = emailService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var supplier = await _companyRepository.GetById(id);

            if (supplier == null)
                return NotFound();

            var supplierResource = _mapper.Map<ManageCompanyResource>(supplier);

            return Ok(supplierResource);
        }

        [HttpGet]
        public async Task<IActionResult> GetByCompany(string companyId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var companySuppliers = await _supplierRepository.GetByCompanyId(companyId);

            if (!companySuppliers.Any()) return BadRequest();

            var suppliers = new List<Company>();
            companySuppliers.ForEach(cs => suppliers.Add(cs.Supplier));

            var supplierResources = _mapper.Map<List<ManageCompanyResource>>(suppliers);

            return Ok(supplierResources);
        }

        [HttpPost]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> CreateSupplier([FromBody] ManageCompanyResource supplierResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(await CheckIfSupplierExits(supplierResource.VATIN))
            {
                return BadRequest(ModelState);
            }

            var supplier = _mapper.Map<Company>(supplierResource);

            supplier.Id = Guid.NewGuid().ToString();
            supplier.CreatedOn = DateTime.Now;
            supplier.CreatedBy = null;

            await _companyRepository.Add(supplier);
            await _unitOfWork.CompleteAsync();
            
            return Ok(supplier);
        }

        [HttpPost]
        [Authorize(Policy = "Admin")]
        [Route("relations")]
        public async Task<IActionResult> CreateSupplierRelation([FromBody] ManageSupplierResource supplierResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var companySupplier = _mapper.Map<CompanySupplier>(supplierResource);

            await _supplierRepository.Add(companySupplier);
            await _unitOfWork.CompleteAsync();

            var supplier = await _companyRepository.GetById(companySupplier.SupplierId);

            return Ok(supplier);
        }

        [HttpPost]
        [Authorize(Policy = "Admin")]
        [Route("{supplierId}/users")]
        public async Task<IActionResult> CreateSupplierUser(string supplierId, [FromBody] SupplierUserResource userResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (await CheckIfSupplierUserExists(userResource.ContactPersonEmail))
            {
                return BadRequest();
            }

            var userIdentity = _mapper.Map<ApplicationUser>(userResource);
            userIdentity.UserName = userIdentity.Email.ToUpper();
            userIdentity.RoleId = "cd0b6c1f-2059-4c67-8e91-f6f8b2a5def2";
            userIdentity.CreatedOn = DateTime.Now;
            userIdentity.CompanyId = supplierId;

            var userPassword = GenerateRandomPassword();
            var result = await _userManager.CreateAsync(userIdentity, userPassword);

            if (!result.Succeeded) return BadRequest(result);

            await _emailService.SendSupplierAccessGrantedMail(userIdentity.Email, userPassword, supplierId);

            return Ok();
        }

        // [HttpPut]
        // [Authorize(Policy = "Admin")]
        // [Route("{supplierId}/users")]
        // public async Task<IActionResult> UpdateSupplierUser(string supplierId, [FromBody] UserResource userResource)
        // {
        //     if (!ModelState.IsValid)
        //     {
        //         return BadRequest(ModelState);
        //     }

        //     var user = await _userManager.FindByEmailAsync(userResource.Email);

        //     if (user == null)
        //         return NotFound();

        //     _mapper.Map<UserResource, ApplicationUser>(userResource, user);

        //     await _unitOfWork.CompleteAsync();

        //     user = await _userManager.Users
        //         .Include(u => u.Role)
        //         .Include(u => u.Language)
        //         .Include(u => u.Company)
        //         .SingleOrDefaultAsync(u => u.NormalizedEmail == userResource.Email.ToUpper());

        //     var updatedUserResource = _mapper.Map<ApplicationUser, UserResource>(user, userResource);

        //     return Ok(updatedUserResource);
        // }

        // [HttpPut("{id}")]
        // [Authorize(Policy = "Admin")]
        // public async Task<IActionResult> Update(string id, [FromBody] ManageCompanyResource supplierResource)
        // {
        //     if (!ModelState.IsValid)
        //     {
        //         return BadRequest(ModelState);
        //     }

        //     var supplier = await _supplierRepository.GetById(id);

        //     if (supplier == null)
        //         return NotFound();

        //     _mapper.Map<ManageCompanyResource, Company>(supplierResource, supplier);

        //     await _unitOfWork.CompleteAsync();

        //     supplier = await _supplierRepository.GetById(id);

        //     var updatedCompanyResource = _mapper.Map<Company, ManageCompanyResource>(supplier, supplierResource);

        //     return Ok(updatedCompanyResource);
        // }

        // [HttpDelete("{id}")]
        // [Authorize(Policy = "Admin")]
        // public async Task<IActionResult> Delete(string id)
        // {
        //     if (!ModelState.IsValid)
        //     {
        //         return BadRequest(ModelState);
        //     }

        //     return Ok();
        // }

        
        private async Task<Boolean> CheckIfSupplierExits(string vatin)
        {
            // TO DO: Implement a VATIN-validator

            var supplierInDB = await _companyRepository.GetByVATIN(vatin);
            if (supplierInDB != null)
            {
                return true;
            }
            return false;
        }
        private Task<bool> CheckIfRelationExits(string vATIN)
        {
            throw new NotImplementedException();
        }
        private async Task<Boolean> CheckIfSupplierUserExists(string ContactPersonEmail)
        {
            var userInDB = await _userManager.FindByEmailAsync(ContactPersonEmail);
            if (userInDB != null)
            {

                return true;
            }
            return false;
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