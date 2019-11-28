using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RespaunceV2.Core.Interfaces;
using RespaunceV2.Core.Models;

namespace RespaunceV2.Infrastructure.Persistence.Repositories
{
    public class SupplierRepository : ISupplierRepository
    {
        private readonly ReportingDbContext _context;
        public SupplierRepository(
            ReportingDbContext context
        )
        {
            _context = context;
        }

        public async Task<CompanySupplier> Add(CompanySupplier supplier)
        {
            await _context.Suppliers.AddAsync(supplier);
            return await Task.FromResult(supplier);
        }

        public Task Delete(CompanySupplier supplier)
        {
            return Task.FromResult(_context.Remove(supplier));
        }

        public async Task<List<CompanySupplier>> GetByCompanyId(string companyId)
        {
            return await _context.Suppliers
                .Where(cs => cs.Company.Id == companyId)
                .Include(cs => cs.Supplier)
                .ThenInclude(s => s.LegalForm)
                .Include(cs => cs.Supplier)
                .ThenInclude(s => s.TypeOfOwnership)
                .ToListAsync();
        }

        public Task Update(CompanySupplier supplier)
        {
            throw new System.NotImplementedException();
        }
    }
}