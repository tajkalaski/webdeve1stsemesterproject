using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RespaunceV2.Core.Interfaces;
using RespaunceV2.Core.Models;

namespace RespaunceV2.Infrastructure.Persistence.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly ReportingDbContext _context;
        public CompanyRepository(ReportingDbContext context)
        {
            this._context = context;
        }
        public async Task<Company> Add(Company company)
        {
            company.CreatedOn = DateTime.Now;
            company.CreatedBy = null;
            await _context.Companies.AddAsync(company);
            return company;
        }

        public Task Delete(Company company)
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<Company>> GetAll()
        {
            var companies = await _context.Companies
                .Include(c => c.LegalForm)
                .Include(c => c.TypeOfOwnership)
                .ToListAsync();
            return companies;
        }

        public Task<Company> GetById(string id)
        {
            return _context.Companies
                .Include(c => c.LegalForm)
                .Include(c => c.TypeOfOwnership)
                .SingleOrDefaultAsync(c => c.Id == id);
        }

        public Task<Company> GetByVATIN(string vatin)
        {
            return _context.Companies
                .Include(c => c.LegalForm)
                .Include(c => c.TypeOfOwnership)
                .SingleOrDefaultAsync(c => c.VATIN == vatin);
        }

        public Task Update(Company company)
        {
            return Task.FromResult(_context.Companies.Update(company));
        }
    }
}