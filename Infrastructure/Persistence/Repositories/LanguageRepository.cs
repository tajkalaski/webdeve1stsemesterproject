using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RespaunceV2.Core.Interfaces;
using RespaunceV2.Core.Models;

namespace RespaunceV2.Infrastructure.Persistence.Repositories
{
    public class LanguageRepository : ILanguageRepository
    {
        private readonly ReportingDbContext _context;
        public LanguageRepository(
            ReportingDbContext context
        )
        {
            _context = context;
        }
        public Task<Language> Add(Language language)
        {
            throw new System.NotImplementedException();
        }

        public Task Delete(Language language)
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<Language>> GetAll()
        {
            return await _context.Languages.ToListAsync();
        }

        public Task<Language> GetById(string id)
        {
            throw new System.NotImplementedException();
        }

        public Task Update(Language language)
        {
            throw new System.NotImplementedException();
        }
    }
}