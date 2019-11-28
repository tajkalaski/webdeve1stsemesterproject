using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RespaunceV2.Core.Interfaces;
using RespaunceV2.Core.Models;
using RespaunceV2.WebApi.Resources;

namespace RespaunceV2.Controllers
{
    [Route("api/assessments")]
    public class AssessmentController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IAssessmentRepository _assessmentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AssessmentController(
            IMapper mapper,
            IAssessmentRepository assessmentRepository,
            IAssessmentQuestionRepository assessmentQuestionRepository,
            IUnitOfWork unitOfWork
        )
        {
            _mapper = mapper;
            _assessmentRepository = assessmentRepository;
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAssessmentResource saveAssessmentResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var assessment = _mapper.Map<Assessment>(saveAssessmentResource);
            assessment = await _assessmentRepository.Add(assessment);

            await _unitOfWork.CompleteAsync();

            var assessmentResource = _mapper.Map<AssessmentResource>(assessment);

            return Ok(assessmentResource);
        }

        [HttpGet]
        public async Task<IActionResult> GetByCompany(string companyId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var assessments = await _assessmentRepository.GetByCompany(companyId);

            if (assessments == null)
                return NotFound();

            var assessmentResource = _mapper.Map<List<AssessmentResource>>(assessments);

            return Ok(assessmentResource);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var assessment = await _assessmentRepository.GetById(id);

            if (assessment == null)
                return NotFound();

            var assessmentResource = _mapper.Map<AssessmentResource>(assessment);

            return Ok(assessmentResource);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] UpdateAssessmentResource updateAssessmentResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var assessment = await _assessmentRepository.GetById(id);

            if (assessment == null)
                return NotFound();

            _mapper.Map<UpdateAssessmentResource, Assessment>(updateAssessmentResource, assessment);
            
            await _unitOfWork.CompleteAsync();

            assessment = await _assessmentRepository.GetById(id);

            var updatedAssessmentResource = _mapper.Map<AssessmentResource>(assessment);

            return Ok(updatedAssessmentResource);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var assessment = await _assessmentRepository.GetById(id);

            if (assessment == null)
                return NotFound();

            await _assessmentRepository.Delete(assessment);
            await _unitOfWork.CompleteAsync();

            return Ok();
        }
    }
}