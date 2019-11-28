using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RespaunceV2.Core.Interfaces;
using RespaunceV2.Core.Models;

namespace RespaunceV2.Infrastructure.Persistence.Repositories
{
    public class QuestionCategoryRepository : IQuestionCategoryRepository
    {
        private readonly ReportingDbContext _context;
        public QuestionCategoryRepository(ReportingDbContext context)
        {
            _context = context;
        }

        public Task<QuestionCategory> Add(QuestionCategory questionCategory)
        {
            throw new NotImplementedException();
        }

        public Task Delete(QuestionCategory questionCategory)
        {
            throw new NotImplementedException();
        }

        public async Task<List<QuestionCategory>> GetAll()
        {
            return await _context.QuestionCategories
                .Include(qc => qc.QuestionSubCategories)
                .ThenInclude(qsc => qsc.Questions)
                .ThenInclude(q => q.SubQuestions)
                .ToListAsync();
        }

        public async Task<QuestionCategory> GetById(string id)
        {
            return await _context.QuestionCategories.SingleOrDefaultAsync(qc => qc.Id == id);
        }

        public Task<List<QuestionCategory>> GetBySubCategories(string[] questionSubCategories)
        {
            throw new NotImplementedException();
        }

        public Task<List<QuestionCategory>> GetBySubCategoryId(string questionSubCategoryId)
        {
            throw new NotImplementedException();
        }

        public Task<QuestionCategory> Update(QuestionCategory questionCategory)
        {
            throw new NotImplementedException();
        }
    }
}