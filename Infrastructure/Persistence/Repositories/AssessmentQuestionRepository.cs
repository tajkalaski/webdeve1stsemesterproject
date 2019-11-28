using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RespaunceV2.Core.Interfaces;
using RespaunceV2.Core.Models;

namespace RespaunceV2.Infrastructure.Persistence.Repositories
{
    public class AssessmentQuestionRepository : IAssessmentQuestionRepository
    {
        private readonly ReportingDbContext _context;
        public AssessmentQuestionRepository(ReportingDbContext context)
        {
            _context = context;
        }

        public async Task<AssessmentQuestion> Add(AssessmentQuestion assessmentQuestion)
        {
            assessmentQuestion.Id = Guid.NewGuid().ToString();
            assessmentQuestion.CreatedOn = DateTime.Now;
            assessmentQuestion.CreatedBy = null;
            await _context.AssessmentQuestions.AddAsync(assessmentQuestion);
            return assessmentQuestion;
        }

        public async Task AddMany(List<AssessmentQuestion> assessmentQuestions)
        {
            assessmentQuestions.ForEach(aq =>
            {
                aq.CreatedOn = DateTime.Now;
                aq.CreatedBy = null;
            });
            
            await _context.AssessmentQuestions.AddRangeAsync(assessmentQuestions);
        }

        public async Task Delete(AssessmentQuestion assessmentQuestion)
        {
            await Task.FromResult(_context.AssessmentQuestions.Remove(assessmentQuestion));
        }

        public void DeleteMany(List<AssessmentQuestion> assessmentQuestions)
        {
            _context.AssessmentQuestions.RemoveRange(assessmentQuestions);
        }

        public async Task<List<AssessmentQuestion>> Get(string[] id)
        {
            var assessmentQuestions = await _context.AssessmentQuestions.ToListAsync();
            return assessmentQuestions;
        }

        public async Task<List<AssessmentQuestion>> GetAll()
        {
            var assessmentQuestions = await _context.AssessmentQuestions
                //.Include(aq => aq.Assessment)
                //.Include(a => a.Question)
                .ToListAsync();
            return assessmentQuestions;
        }

        public async Task<List<AssessmentQuestion>> GetByAssessmentAsync(string assessmentId)
        {
            var assessmentQuestions = await _context.AssessmentQuestions
                .Where(aq => aq.AssessmentId == assessmentId)
                .ToListAsync();
            return assessmentQuestions;
        }

        public async Task<AssessmentQuestion> GetById(string id)
        {
            var assessmentQuestions = await _context.AssessmentQuestions
                //.Include(aq => aq.Assessment)
                .Include(a => a.SubQuestion)
                .SingleOrDefaultAsync(a => a.Id == id);
            return assessmentQuestions;
        }

        public async Task<List<AssessmentQuestion>> GetByUser(string userId)
        {
            
            var assessmentQuestions = await _context.AssessmentQuestions
                .Where(a => a.ResponsiblePerson.ApplicationUser.Id == userId)
                .Include(aq => aq.ResponsiblePerson)
                .Include(aq => aq.SubQuestion.Question)
                .ToListAsync();
            return assessmentQuestions;
        }

        public Task Update(AssessmentQuestion assessmentQuestion)
        {
            throw new System.NotImplementedException();
        }
    }
}