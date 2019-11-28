using System.Collections.Generic;
using System.Threading.Tasks;
using RespaunceV2.Core.Models;

namespace RespaunceV2.Core.Interfaces
{
    public interface IUserRepository
    {
        Task<ApplicationUser> GetById(string id);
        Task<List<ApplicationUser>> GetByCompany(string companyId);
        Task<List<ApplicationUser>> GetAll();
        Task<ApplicationUser> Add(ApplicationUser entity);
        Task Update(ApplicationUser entity);
        Task Delete(ApplicationUser entity);
        Task<ApplicationUser> Create(string firstName, string lastName, string email, string userName, string password);
    }
}