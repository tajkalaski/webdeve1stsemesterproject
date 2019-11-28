using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RespaunceV2.Core.Interfaces;
using RespaunceV2.WebApi.Resources;

namespace RespaunceV2.Controllers
{
    [Route("api/questions")]
    public class QuestionController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IQuestionRepository _questionRepository;
        private readonly IQuestionSubCategoryRepository _questionSubCategoryRepository;
        private readonly IQuestionCategoryRepository _questionCategoryRepository;

        public QuestionController(
            IMapper mapper,
            IQuestionRepository questionRepository,
            IQuestionSubCategoryRepository questionSubCategoryRepository,
            IQuestionCategoryRepository questionCategoryRepository
        )
        {
            _mapper = mapper;
            _questionRepository = questionRepository;
            _questionSubCategoryRepository = questionSubCategoryRepository;
            _questionCategoryRepository = questionCategoryRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var questions = await _questionRepository.GetAll();
            var questionResources = _mapper.Map<List<QuestionResource>>(questions);

            return Ok(questionResources);
        }
    }
}