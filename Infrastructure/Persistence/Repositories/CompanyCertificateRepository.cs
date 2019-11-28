using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RespaunceV2.Core.Interfaces;
using RespaunceV2.Core.Models;

namespace RespaunceV2.Infrastructure.Persistence.Repositories
{
    public class CompanyCertificateRepository : ICompanyCertificateRepository
    {
        private readonly ReportingDbContext _context;
        public CompanyCertificateRepository(
            ReportingDbContext context
        )
        {
            _context = context;
        }

        public async Task<CompanyCertificate> Add(CompanyCertificate companyCertificate)
        {
            await _context.CompanyCertificates.AddAsync(companyCertificate);
            return companyCertificate;
        }

        public Task Delete(CompanyCertificate companyCertificate)
        {
            var deletedCompanyCertificate = _context.CompanyCertificates.Remove(companyCertificate);
            return Task.FromResult(deletedCompanyCertificate);
        }

        public async Task<List<CompanyCertificate>> GetByCompany(string companyId)
        {
            var companyCertificates = await _context.CompanyCertificates
                .Where(cc => cc.CompanyId == companyId)
                .Include(cc => cc.Certificate)
                .ToListAsync();
            return companyCertificates;
        }

        public async Task<CompanyCertificate> GetById(string id)
        {
            var companyCertificate = await _context.CompanyCertificates
                .Include(cc => cc.Certificate)
                .SingleOrDefaultAsync(cc => cc.Id == id);
                
            return companyCertificate;
        }

        public async Task<List<CompanyCertificate>> GetBySupplier(string supplierId)
        {
            var companyCertificates = await _context.CompanyCertificates
                .Where(cc => cc.CompanyId == supplierId)
                .Include(cc => cc.Certificate)
                .ToListAsync();
            return companyCertificates;
        }

        public Task Update(CompanyCertificate companyCertificate)
        {
            throw new System.NotImplementedException();
        }
    }
}