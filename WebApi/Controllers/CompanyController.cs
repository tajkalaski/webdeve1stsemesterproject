using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RespaunceV2.Core.Interfaces;
using RespaunceV2.Core.Models;
using RespaunceV2.WebApi.Resources;

namespace RespaunceV2.Controllers
{
    [Route("api/companies")]
    public class CompanyController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ICompanyRepository _companyRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CompanyController(
            IMapper mapper,
            ICompanyRepository companyRepository,
            IUnitOfWork unitOfWork
        )
        {
            _mapper = mapper;
            _companyRepository = companyRepository;
            _unitOfWork = unitOfWork;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var company = await _companyRepository.GetById(id);

            if (company == null)
                return NotFound();
                
            var companyResource = _mapper.Map<ManageCompanyResource>(company);

            return Ok(companyResource);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var companies = await _companyRepository.GetAll();
            var companiesResource = _mapper.Map<List<ManageCompanyResource>>(companies);

            return Ok(companiesResource);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ManageCompanyResource companyResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var company = _mapper.Map<Company>(companyResource);
            company = await _companyRepository.Add(company);
            await _unitOfWork.CompleteAsync();

            return Ok(company);
        }

        [HttpPut("{id}")]
        [Authorize(Policy="AdminOrSupplier")]
        public async Task<IActionResult> Update(string id, [FromBody] ManageCompanyResource companyResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var company = await _companyRepository.GetById(id);

            if (company == null)
                return NotFound();

            _mapper.Map<ManageCompanyResource, Company>(companyResource, company);

            await _unitOfWork.CompleteAsync();

            company = await _companyRepository.GetById(id);

            var updatedCompanyResource = _mapper.Map<Company, ManageCompanyResource>(company, companyResource);

            return Ok(updatedCompanyResource);
        }
    }
}