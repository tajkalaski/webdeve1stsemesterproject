using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RespaunceV2.Core.Interfaces;
using RespaunceV2.Core.Models;

namespace RespaunceV2.Infrastructure.Persistence.Repositories
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly ReportingDbContext _context;

        public QuestionRepository(
            ReportingDbContext context
        )
        {
            _context = context;
        }

        public Task<Question> Add(Question entity)
        {
            throw new System.NotImplementedException();
        }

        public Task Delete(Question entity)
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<Question>> GetAll()
        {
            return await _context.Questions
                .Include(q => q.SubQuestions)
                .ToListAsync();
        }

        public Task<Question> GetById(string id)
        {
            throw new System.NotImplementedException();
        }

        public Task Update(Question entity)
        {
            throw new System.NotImplementedException();
        }
    }
}