using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RespaunceV2.Core.Interfaces;
using RespaunceV2.WebApi.Resources;

namespace RespaunceV2.Controllers
{
    [Route("api/languages")]
    public class LanguageController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ILanguageRepository _languageRepository;
        public LanguageController(
            IMapper mapper,
            ILanguageRepository languageRepository
        )
        {
            _mapper = mapper;
            _languageRepository = languageRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var languages = await _languageRepository.GetAll();
            var languagesResource = _mapper.Map<List<LanguageResource>>(languages);
            return Ok(languagesResource);
        }
    }
}