using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RespaunceV2.Core.Interfaces;
using RespaunceV2.Core.Models;

namespace RespaunceV2.Infrastructure.Persistence.Repositories
{
    public class AssessmentRepository : IAssessmentRepository
    {
        private readonly ReportingDbContext _context;
        public AssessmentRepository(ReportingDbContext context)
        {
            _context = context;
        }

        public async Task<Assessment> Add(Assessment assessment)
        {
            assessment.CreatedOn = DateTime.Now;
            assessment.CreatedBy = null;
            assessment.AssessmentQuestions.ForEach(aq => {
                aq.CreatedOn = DateTime.Now;
                aq.CreatedBy = null;
            });
            await _context.Assessments.AddAsync(assessment);
            return assessment;
        }

        public Task Delete(Assessment assessment)
        {
            return Task.FromResult(_context.Remove(assessment));
        }

        public async Task<List<Assessment>> GetAll()
        {
            var assessments = await _context.Assessments
                .Include(a => a.Company)
                .Include(a => a.AssessmentQuestions)
                .ThenInclude(aq => aq.SubQuestion)
                .ToListAsync();
            return assessments;
        }

        public async Task<Assessment> GetById(string id)
        {
            var assesments = await _context.Assessments
                .Include(a => a.Company)
                .Include(a => a.AssessmentQuestions)
                .ThenInclude(aq => aq.ResponsiblePerson)
                .ThenInclude(rp => rp.ApplicationUser)
                .SingleOrDefaultAsync(a => a.Id == id);
            return assesments;
        }

        public async Task<List<Assessment>> Get(string[] ids)
        {
            var assesments = await _context.Assessments.Where(a => ids.Contains(a.Id ?? null)).ToListAsync();
            return assesments;
        }

        public Task Update(Assessment assessment)
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<Assessment>> GetByCompany(string companyId)
        {
            var assesments = await _context.Assessments
                .Where(a => a.Company.Id == companyId)
                .ToListAsync();
            return assesments;
        }
    }
}