using System.Collections.Generic;
using System.Threading.Tasks;
using RespaunceV2.Core.Models;

namespace RespaunceV2.Core.Interfaces
{
    public interface ICompanyCertificateRepository
    {
        Task<CompanyCertificate> GetById(string id);
        Task<List<CompanyCertificate>> GetBySupplier(string supplierId);
        Task<List<CompanyCertificate>> GetByCompany(string companyId);
        Task<CompanyCertificate> Add(CompanyCertificate companyCertificate);
        Task Update(CompanyCertificate companyCertificate);
        Task Delete(CompanyCertificate companyCertificate);
    }
}