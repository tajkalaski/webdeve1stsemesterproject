using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RespaunceV2.Core.Interfaces;
using RespaunceV2.Core.Models;

namespace RespaunceV2.Infrastructure.Persistence.Repositories
{
    public class TypeOfOwnershipRepository : ITypeOfOwnershipRepository
    {
        private readonly ReportingDbContext _context;
        public TypeOfOwnershipRepository(
            ReportingDbContext context
        )
        {
            _context = context;
        }

        public Task<TypeOfOwnership> Add(TypeOfOwnership typeOfOwnership)
        {
            throw new System.NotImplementedException();
        }

        public Task Delete(TypeOfOwnership typeOfOwnership)
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<TypeOfOwnership>> GetAll()
        {
            return await _context.TypesOfOwnership.ToListAsync();
        }

        public Task<TypeOfOwnership> GetById(string id)
        {
            throw new System.NotImplementedException();
        }

        public Task Update(TypeOfOwnership typeOfOwnership)
        {
            throw new System.NotImplementedException();
        }
    }
}