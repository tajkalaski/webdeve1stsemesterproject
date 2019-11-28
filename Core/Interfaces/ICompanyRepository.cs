using System.Collections.Generic;
using System.Threading.Tasks;
using RespaunceV2.Core.Models;

namespace RespaunceV2.Core.Interfaces
{
    public interface ICompanyRepository
    {
        Task<Company> GetById(string id);
        Task<Company> GetByVATIN(string vatin);
        Task<List<Company>> GetAll();
        Task<Company> Add(Company company);
        Task Update(Company company);
        Task Delete(Company company);
    }
}