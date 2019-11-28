using System.Threading.Tasks;
using RespaunceV2.Core.Models;

namespace RespaunceV2.Core.Interfaces
{
    public interface IRefreshTokenRepository
    {
        Task<string> CreateToken(string id, string remoteIpAddress);
        Task<RefreshToken> GetByUserId(string id);
        Task<RefreshToken> GetByToken(string token);
        void Delete(RefreshToken refreshToken);
        //Task Deactivate(RefreshToken refreshToken);



    }
}