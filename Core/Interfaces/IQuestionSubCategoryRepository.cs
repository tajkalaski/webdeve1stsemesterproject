using System.Collections.Generic;
using System.Threading.Tasks;
using RespaunceV2.Core.Models;

namespace RespaunceV2.Core.Interfaces
{
    public interface IQuestionSubCategoryRepository
    {
        Task<QuestionSubCategory> GetById(string id);
        Task<List<QuestionSubCategory>> GetByQuestionId(string questionId);
        Task<List<QuestionSubCategory>> GetByQuestions(List<Question> questions);
        Task<List<QuestionSubCategory>> GetAll();
        Task<QuestionSubCategory> Add(QuestionSubCategory questionCategory);
        Task<QuestionSubCategory> Update(QuestionSubCategory questionCategory);
        Task Delete(QuestionSubCategory questionCategory);
    }
}