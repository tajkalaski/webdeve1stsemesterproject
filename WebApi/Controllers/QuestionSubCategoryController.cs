using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RespaunceV2.Core.Interfaces;

namespace RespaunceV2.Controllers
{
    [Route("api/questionSubCategories")]
    public class QuestionSubCategoryController : Controller
    {
        private readonly IQuestionSubCategoryRepository _questionSubCatgoryRepository;

        public QuestionSubCategoryController(
            IQuestionSubCategoryRepository questionSubCatgoryRepository
        )
        {
            _questionSubCatgoryRepository = questionSubCatgoryRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var subCategories = await _questionSubCatgoryRepository.GetAll();
            return Ok(subCategories);
        }
    }
}