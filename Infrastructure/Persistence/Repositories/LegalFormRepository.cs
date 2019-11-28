using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RespaunceV2.Core.Interfaces;
using RespaunceV2.Core.Models;

namespace RespaunceV2.Infrastructure.Persistence.Repositories
{
    public class LegalFormRepository : ILegalFormRepository
    {
        private readonly ReportingDbContext _context;
        public LegalFormRepository(
            ReportingDbContext context
        )
        {
            _context = context;
        }

        public Task<LegalForm> Add(LegalForm legalForm)
        {
            throw new System.NotImplementedException();
        }

        public Task Delete(LegalForm legalForm)
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<LegalForm>> GetAll()
        {
            return await _context.LegalForms.ToListAsync();
        }

        public Task<LegalForm> GetById(string id)
        {
            throw new System.NotImplementedException();
        }

        public Task Update(LegalForm legalForm)
        {
            throw new System.NotImplementedException();
        }
    }
}