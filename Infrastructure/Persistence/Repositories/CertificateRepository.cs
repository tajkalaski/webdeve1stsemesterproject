using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RespaunceV2.Core.Interfaces;
using RespaunceV2.Core.Models;

namespace RespaunceV2.Infrastructure.Persistence.Repositories
{
    public class CertificateRepository : ICertificateRepository
    {
        private readonly ReportingDbContext _context;
        public CertificateRepository(
            ReportingDbContext context
        )
        {
            _context = context;
        }

        public Task<Certificate> Add(Certificate certificate)
        {
            throw new System.NotImplementedException();
        }

        public Task Delete(Certificate certificate)
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<Certificate>> GetAll()
        {
            var certificates = await _context.Certificates
                .ToListAsync();
            
            return certificates;
        }

        public Task<Certificate> GetById(string id)
        {
            throw new System.NotImplementedException();
        }

        public Task Update(Certificate certificate)
        {
            throw new System.NotImplementedException();
        }
    }
}