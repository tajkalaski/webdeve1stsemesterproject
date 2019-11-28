using System.Threading.Tasks;
using RespaunceV2.Core.Interfaces;

namespace RespaunceV2.Infrastructure.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ReportingDbContext _context;

        public UnitOfWork(ReportingDbContext context)
        {
            this._context = context;
        }
        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}