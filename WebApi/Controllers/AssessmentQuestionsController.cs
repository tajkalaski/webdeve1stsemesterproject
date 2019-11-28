using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RespaunceV2.Core.Interfaces;
using RespaunceV2.Core.Models;
using RespaunceV2.WebApi.Resources;

namespace RespaunceV2.Controllers
{
    [Route("api/assessmentQuestions")]
    public class AssessmentQuestionsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAssessmentQuestionRepository _assessmentQuestionRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AssessmentQuestionsController(
            IMapper mapper,
            UserManager<ApplicationUser> userManager,
            IAssessmentQuestionRepository assessmentQuestionRepository,
            IUnitOfWork unitOfWork
        )
        {
            _mapper = mapper;
            _userManager = userManager;
            _assessmentQuestionRepository = assessmentQuestionRepository;
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        [Route("createMany")]
        public async Task<IActionResult> CreateMany([FromBody] List<AssessmentQuestionResource> addedAssessmentQuestionResources)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!addedAssessmentQuestionResources.Any())
            {
                return BadRequest(ModelState);
            }

            var addedAssessmentQuestions = _mapper.Map<List<AssessmentQuestion>>(addedAssessmentQuestionResources);
            await _assessmentQuestionRepository.AddMany(addedAssessmentQuestions);

            await _unitOfWork.CompleteAsync();
            return Ok();
        }

        [HttpPost]
        [Route("deleteMany")]
        public async Task<IActionResult> DeleteMany([FromBody] List<AssessmentQuestionResource> deletedAssessmentQuestionResources)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            if (!deletedAssessmentQuestionResources.Any())
            {
                return BadRequest(ModelState);
            }
            
            var deletedAssessmentQuestions = _mapper.Map<List<AssessmentQuestion>>(deletedAssessmentQuestionResources);
            _assessmentQuestionRepository.DeleteMany(deletedAssessmentQuestions);

            await _unitOfWork.CompleteAsync();
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetByAssessment(string assessmentId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var assessmentQuestions = await _assessmentQuestionRepository.GetByAssessmentAsync(assessmentId);
            var assessmentQuestionsResource = _mapper.Map<List<AssessmentQuestionResource>>(assessmentQuestions);

            return Ok(assessmentQuestionsResource);
        }

        [HttpGet]
        [Route("user/{email}")]
        public async Task<IActionResult> GetByUser(string email)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return NotFound();
            }

            var assessmentQuestions = await _assessmentQuestionRepository.GetByUser(user.Id);
            var assessmentQuestionResources = _mapper.Map<List<AssessmentQuestionResource>>(assessmentQuestions);

            return Ok(assessmentQuestionResources);
        }
    }
}