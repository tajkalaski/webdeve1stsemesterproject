using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RespaunceV2.Core.Interfaces;
using RespaunceV2.Core.Models;
using RespaunceV2.WebApi.Resources;

namespace RespaunceV2.Controllers{
[Route("api/dataEntries")]
 public class DataEntryController : Controller{
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IDataEntryRepository _dataentryRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthService _authService;

            public DataEntryController(
            IMapper mapper,
            UserManager<ApplicationUser> userManager,
            IDataEntryRepository dataentryRepository,
            IUnitOfWork unitOfWork,
            IAuthService authService

        )
        {
            _mapper = mapper;
            _userManager = userManager;
            _dataentryRepository = dataentryRepository;
            _unitOfWork = unitOfWork;
            _authService = authService;
        }

         [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dataEntries = await _dataentryRepository.GetAll();
            return Ok(dataEntries);
        }

         [HttpPost]
        public async Task<IActionResult> Create([FromBody] DataEntry dataEntry)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // var dataEntry = _mapper.Map<DataEntry>(dataEntry);
            dataEntry = await _dataentryRepository.Add(dataEntry);
            await _unitOfWork.CompleteAsync();

            return Ok(dataEntry);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dataEntry = await _dataentryRepository.GetById(id);

            if (dataEntry == null)
                return NotFound();
                
            await _dataentryRepository.Delete(dataEntry);
            await _unitOfWork.CompleteAsync();

            return Ok();
        }

           [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id,[FromBody] DataEntry dataEntry)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dataentry = await _dataentryRepository.GetById(id);

            if (dataentry == null)
                return NotFound();

           // _mapper.Map<CompanyCertificateResource, CompanyCertificate>(dataEntryResource, dataEntry);

            await _unitOfWork.CompleteAsync();

            dataentry = await _dataentryRepository.GetById(id);

            // var updatedCompanyResource = _mapper.Map<CompanyCertificate, CompanyCertificateResource>(companyCertificate, companyCertificateResource);

            return Ok(dataentry);
        }
    }

}