using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RespaunceV2.Core.Interfaces;
using RespaunceV2.Core.Models;

namespace RespaunceV2.Infrastructure.Persistence.Repositories
{
    public class QuestionSubCategoryRepository : IQuestionSubCategoryRepository
    {
        private readonly ReportingDbContext _context;

        public QuestionSubCategoryRepository(
            ReportingDbContext context
        )
        {
            _context = context;
        }

        public Task<QuestionSubCategory> Add(QuestionSubCategory questionCategory)
        {
            throw new System.NotImplementedException();
        }

        public Task Delete(QuestionSubCategory questionCategory)
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<QuestionSubCategory>> GetAll()
        {
            return await _context.QuestionSubCategories.ToListAsync();
        }

        public Task<QuestionSubCategory> GetById(string id)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<QuestionSubCategory>> GetByQuestionId(string questionId)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<QuestionSubCategory>> GetByQuestions(List<Question> questions)
        {
            throw new System.NotImplementedException();
        }

        public Task<QuestionSubCategory> Update(QuestionSubCategory questionCategory)
        {
            throw new System.NotImplementedException();
        }
    }
}