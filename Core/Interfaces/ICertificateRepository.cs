using System.Collections.Generic;
using System.Threading.Tasks;
using RespaunceV2.Core.Models;

namespace RespaunceV2.Core.Interfaces
{
    public interface ICertificateRepository
    {
        Task<Certificate> GetById(string id);
        Task<List<Certificate>> GetAll();
        Task<Certificate> Add(Certificate certificate);
        Task Update(Certificate certificate);
        Task Delete(Certificate certificate);
    }
}