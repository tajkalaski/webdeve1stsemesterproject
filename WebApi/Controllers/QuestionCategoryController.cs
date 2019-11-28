using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RespaunceV2.Core.Interfaces;
using RespaunceV2.WebApi.Resources;

namespace RespaunceV2.Controllers
{
    [Route("api/questionCategories")]
    public class QuestionCategoryController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IQuestionCategoryRepository _questionCategoryRepository;

        public QuestionCategoryController(
            IMapper mapper,
            IQuestionCategoryRepository questionCategoryRepository
        )
        {
            _mapper = mapper;
            _questionCategoryRepository = questionCategoryRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var questionCategories = await _questionCategoryRepository.GetAll();
            var questionCategoriesResource = _mapper.Map<List<QuestionCategoryResource>>(questionCategories);
            return Ok(questionCategoriesResource);
        }
    }
}