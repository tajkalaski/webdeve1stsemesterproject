using System.Collections.Generic;
using System.Threading.Tasks;
using RespaunceV2.Core.Models;

namespace RespaunceV2.Core.Interfaces
{
    public interface IRoleRepository
    {
        Task<Role> GetById(string id);
        Task<List<Role>> GetAll();
        Task<Role> Add(Role role);
        Task Update(Role role);
        Task Delete(Role role);
    }
}