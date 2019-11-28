using System.Collections.Generic;
using System.Threading.Tasks;

namespace RespaunceV2.Core.Interfaces
{
    public interface IRepository<T>
    {
        Task<T> GetById(string id);
        Task<List<T>> GetAll();
        Task<T> Add(T entity);
        Task Update(T entity);
        Task Delete(T entity);
    }
}