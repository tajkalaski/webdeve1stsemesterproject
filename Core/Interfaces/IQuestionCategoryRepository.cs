using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RespaunceV2.Core.Models;

namespace RespaunceV2.Core.Interfaces
{
    public interface IQuestionCategoryRepository
    {
        Task<QuestionCategory> GetById(string id);
        Task<List<QuestionCategory>> GetBySubCategoryId(string questionSubCategoryId);
        Task<List<QuestionCategory>> GetBySubCategories(string[] questionSubCategories);
        Task<List<QuestionCategory>> GetAll();
        Task<QuestionCategory> Add(QuestionCategory questionCategory);
        Task<QuestionCategory> Update(QuestionCategory questionCategory);
        Task Delete(QuestionCategory questionCategory);
    }
}