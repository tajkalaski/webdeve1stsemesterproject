using System.Collections.Generic;
using System.Threading.Tasks;
using RespaunceV2.Core.Models;

namespace RespaunceV2.Core.Interfaces
{
    public interface ILanguageRepository
    {
        Task<Language> GetById(string id);
        Task<List<Language>> GetAll();
        Task<Language> Add(Language language);
        Task Update(Language language);
        Task Delete(Language language);
    }
}