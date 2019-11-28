using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using RespaunceV2.Core.Models;

namespace RespaunceV2.Core.Interfaces
{
    public interface IAuthService
    {
        Task<ApplicationUser> GetUserFromAuthHeader(HttpRequest request);
    }
}