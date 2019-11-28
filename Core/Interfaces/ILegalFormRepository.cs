using System.Collections.Generic;
using System.Threading.Tasks;
using RespaunceV2.Core.Models;

namespace RespaunceV2.Core.Interfaces
{
    public interface ILegalFormRepository
    {
        Task<LegalForm> GetById(string id);
        Task<List<LegalForm>> GetAll();
        Task<LegalForm> Add(LegalForm legalForm);
        Task Update(LegalForm legalForm);
        Task Delete(LegalForm legalForm);
    }
}