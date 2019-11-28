using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RespaunceV2.Core.Interfaces;
using RespaunceV2.WebApi.Resources;

namespace RespaunceV2.Controllers
{
    [Route("api/legalForms")]
    public class LegalFormController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ILegalFormRepository _legalFormRepository;
        public LegalFormController(
           IMapper mapper,
           ILegalFormRepository legalFormRepository 
        )
        {
            _legalFormRepository = legalFormRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var legalForms = await _legalFormRepository.GetAll();
            var result = _mapper.Map<List<LegalFormResource>>(legalForms);
            return Ok(result);
        }
    }
}