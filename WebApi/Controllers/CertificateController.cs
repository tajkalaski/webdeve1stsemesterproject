using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RespaunceV2.Core.Interfaces;
using RespaunceV2.Core.Models;
using RespaunceV2.WebApi.Resources;

namespace WebApi.Controllers
{
    [Route("api/certificates")]
    public class CertificateController : Controller
    {
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICertificateRepository _certificateRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthService _authService;

        public CertificateController(
            IMapper mapper,
            UserManager<ApplicationUser> userManager,
            ICertificateRepository certificateRepository,
            IUnitOfWork unitOfWork,
            IAuthService authService

        )
        {
            _mapper = mapper;
            _userManager = userManager;
            _certificateRepository = certificateRepository;
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

            var certificates = await _certificateRepository.GetAll();
            var certificateResources = _mapper.Map<List<CertificateResource>>(certificates);
            return Ok(certificateResources);
        }
    }
}