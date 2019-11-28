using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RespaunceV2.Core.Interfaces;
using RespaunceV2.Core.Models;

namespace RespaunceV2.Infrastructure.Persistence.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly ReportingDbContext _context;
        public RoleRepository(
            ReportingDbContext context
        )
        {
            _context = context;
        }

        public Task<Role> Add(Role role)
        {
            throw new System.NotImplementedException();
        }

        public Task Delete(Role role)
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<Role>> GetAll()
        {
            return await _context.Roles.ToListAsync();
        }

        public Task<Role> GetById(string id)
        {
            throw new System.NotImplementedException();
        }

        public Task Update(Role role)
        {
            throw new System.NotImplementedException();
        }
    }
}