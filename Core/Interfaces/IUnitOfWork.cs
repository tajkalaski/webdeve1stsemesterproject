using System.Threading.Tasks;

namespace RespaunceV2.Core.Interfaces
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
    }
}