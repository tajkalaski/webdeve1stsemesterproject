using System.Collections.Generic;
using System.Threading.Tasks;
using RespaunceV2.Core.Models;

namespace RespaunceV2.Core.Interfaces
{
    public interface ITypeOfOwnershipRepository
    {
        Task<TypeOfOwnership> GetById(string id);
        Task<List<TypeOfOwnership>> GetAll();
        Task<TypeOfOwnership> Add(TypeOfOwnership typeOfOwnership);
        Task Update(TypeOfOwnership typeOfOwnership);
        Task Delete(TypeOfOwnership typeOfOwnership);
    }
}