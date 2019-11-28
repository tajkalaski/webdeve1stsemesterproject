using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RespaunceV2.Core.Interfaces;
using RespaunceV2.Core.Models;
using RespaunceV2.WebApi.Resources;

namespace RespaunceV2.WebApi.Controllers
{
    [Route("api/companyCertificates")]
    public class CompanyCertificateController : Controller
    {
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICompanyCertificateRepository _companyCertificateRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthService _authService;

        public CompanyCertificateController(
            IMapper mapper,
            UserManager<ApplicationUser> userManager,
            ICompanyCertificateRepository companyCertificateRepository,
            IUnitOfWork unitOfWork,
            IAuthService authService

        )
        {
            _mapper = mapper;
            _userManager = userManager;
            _companyCertificateRepository = companyCertificateRepository;
            _unitOfWork = unitOfWork;
            _authService = authService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CompanyCertificateResource companyCertificateResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _authService.GetUserFromAuthHeader(Request);

            if (user == null)
            {
                return Unauthorized();
            }

            var companyCertificate = _mapper.Map<CompanyCertificate>(companyCertificateResource);
            
            companyCertificate.CreatedOn = DateTime.Now;
            companyCertificate.CreatedById = user.Id;
            companyCertificate.CompanyId = user.CompanyId;

            companyCertificate = await _companyCertificateRepository.Add(companyCertificate);
            await _unitOfWork.CompleteAsync();

            return Ok(companyCertificate);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var companyCertificate = await _companyCertificateRepository.GetById(id);

            if (companyCertificate == null)
                return NotFound();
                
            var companyCertificateResource = _mapper.Map<CompanyCertificateResource>(companyCertificate);

            return Ok(companyCertificateResource);
        }

        [HttpGet()]
        public async Task<IActionResult> GetByCompany(string companyId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var companyCertificate = await _companyCertificateRepository.GetByCompany(companyId);

            if (companyCertificate == null)
                return NotFound();
                
            var companyCertificateResource = _mapper.Map<List<CompanyCertificateResource>>(companyCertificate);

            return Ok(companyCertificateResource);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id,[FromBody] CompanyCertificateResource companyCertificateResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var companyCertificate = await _companyCertificateRepository.GetById(id);

            if (companyCertificate == null)
                return NotFound();

            _mapper.Map<CompanyCertificateResource, CompanyCertificate>(companyCertificateResource, companyCertificate);

            await _unitOfWork.CompleteAsync();

            companyCertificate = await _companyCertificateRepository.GetById(id);

            var updatedCompanyResource = _mapper.Map<CompanyCertificate, CompanyCertificateResource>(companyCertificate, companyCertificateResource);

            return Ok(updatedCompanyResource);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var companyCertificate = await _companyCertificateRepository.GetById(id);

            if (companyCertificate == null)
                return NotFound();
                
            await _companyCertificateRepository.Delete(companyCertificate);
            await _unitOfWork.CompleteAsync();

            return Ok();
        }
    }
}