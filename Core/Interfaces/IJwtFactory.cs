using System.Security.Claims;
using System.Threading.Tasks;

namespace RespaunceV2.Core.Interfaces
{
    public interface IJwtFactory
    {
        Task<string> GenerateEncodedToken(string id, string email, string role);
        ClaimsIdentity GenerateClaimsIdentity(string id, string email, string role);
    }
}