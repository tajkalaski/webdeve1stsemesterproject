using System.Collections.Generic;
using System.Threading.Tasks;
using RespaunceV2.Core.Models;

namespace RespaunceV2.Core.Interfaces
{
    public interface IAssessmentQuestionRepository
    {
        Task<AssessmentQuestion> GetById(string id);
        Task<List<AssessmentQuestion>> GetAll();
        Task<List<AssessmentQuestion>> Get(string[] id);
        Task<List<AssessmentQuestion>> GetByAssessmentAsync(string assessmentId);
        Task<List<AssessmentQuestion>> GetByUser(string userId);
        Task<AssessmentQuestion> Add(AssessmentQuestion assessmentQuestion);
        Task AddMany(List<AssessmentQuestion> assessmentQuestions);
        Task Update(AssessmentQuestion assessmentQuestion);
        Task Delete(AssessmentQuestion assessmentQuestion);
        void DeleteMany(List<AssessmentQuestion> assessmentQuestions);
    }
}