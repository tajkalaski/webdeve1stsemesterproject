using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RespaunceV2.Core.Interfaces;
using RespaunceV2.WebApi.Resources;

namespace RespaunceV2.Controllers
{
    [Route("api/roles")]
    public class RoleController : Controller
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;
        public RoleController(
            IRoleRepository roleRepository,
            IMapper mapper
        )
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var roles = await _roleRepository.GetAll();
            roles.RemoveAll(r => r.Id == "cd0b6c1f-2059-4c67-8e91-f6f8b2a5def2");
            var rolesResource = _mapper.Map<List<RoleResource>>(roles);
            return Ok(rolesResource);
        }
    }
}