using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RespaunceV2.Core.Interfaces;
using RespaunceV2.WebApi.Resources;

namespace RespaunceV2.Controllers
{
    [Route("api/typesOfOwnership")]
    public class TypeOfOwnershipController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ITypeOfOwnershipRepository _typeOfOwnershipRepository;
        public TypeOfOwnershipController(
           IMapper mapper,
           ITypeOfOwnershipRepository typeOfOwnershipRepository
        )
        {
            _typeOfOwnershipRepository = typeOfOwnershipRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var typesOfOwnership = await _typeOfOwnershipRepository.GetAll();
            var result = _mapper.Map<List<TypeOfOwnershipResource>>(typesOfOwnership);
            return Ok(result);
        }
    }
}