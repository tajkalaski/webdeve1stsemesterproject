using System.Collections.Generic;
using System.Threading.Tasks;
using RespaunceV2.Core.Models;

namespace RespaunceV2.Core.Interfaces
{
    public interface IAssessmentRepository
    {
        Task<Assessment> GetById(string id);
        Task<List<Assessment>> GetAll();
        Task<List<Assessment>> Get(string[] id);
        Task<List<Assessment>> GetByCompany(string companyId);
        Task<Assessment> Add(Assessment assessment);
        Task Update(Assessment assessment);
        Task Delete(Assessment assessment);
    }
}