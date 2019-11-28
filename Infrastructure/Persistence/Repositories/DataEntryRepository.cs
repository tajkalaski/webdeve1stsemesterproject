using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RespaunceV2.Core.Interfaces;
using RespaunceV2.Core.Models;

namespace RespaunceV2.Infrastructure.Persistence.Repositories
{
    public class DataDentryRepository : IDataEntryRepository
    {
        private readonly ReportingDbContext _context;
        public DataDentryRepository(ReportingDbContext context)
        {
            this._context = context;
        }


        public async Task<DataEntry> Add(DataEntry dataentry)
        {
            await _context.DataEntries.AddAsync(dataentry);
            return dataentry;
        }

        public Task Delete(DataEntry dataentry)
        {
            var deletedDataEntry = _context.DataEntries.Remove(dataentry);
            return Task.FromResult(deletedDataEntry);
        }

        public async Task<List<DataEntry>> GetAll()
        {
            var dataEntries = await _context.DataEntries.ToListAsync();
            return dataEntries;
        }

        public  Task<List<DataEntry>> GetByCompany(string companyId)
        {
            throw new NotImplementedException();
        }

        public Task<List<DataEntry>> GetByDivisions(string[] DivisionIds)
        {
            throw new NotImplementedException();
        }

        public async Task<DataEntry> GetById(string id)
        {
            var dataEntry = await _context.DataEntries
                .Include(de => de.Subquestion)
                .SingleOrDefaultAsync(de => de.Id == id);
                
            return dataEntry;
        }

        public async Task<List<DataEntry>> GetByWorksites(string[] WorksiteIds)
        {
            var worksitesdataentries = await _context.DataEntries.Where(a => WorksiteIds.Contains(a.Id ?? null)).ToListAsync();
            return worksitesdataentries;
        }

        public Task Update(DataEntry dataentry)
        {
             return Task.FromResult(_context.DataEntries.Update(dataentry));
        }
    }
}