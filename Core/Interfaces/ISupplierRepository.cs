using System.Collections.Generic;
using System.Threading.Tasks;
using RespaunceV2.Core.Models;

namespace RespaunceV2.Core.Interfaces
{
    public interface ISupplierRepository
    {
        Task<CompanySupplier> Add(CompanySupplier supplier);
        Task<List<CompanySupplier>> GetByCompanyId(string companyId);
        Task Update(CompanySupplier supplier);
        Task Delete(CompanySupplier supplier);
    }
}