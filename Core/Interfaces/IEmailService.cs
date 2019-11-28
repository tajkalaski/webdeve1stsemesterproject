using System.Threading.Tasks;

namespace RespaunceV2.Core.Interfaces
{
    public interface IEmailService
    {
        Task SendAccessGrantedMail(string name, string email, string password, string companyName);
        Task SendSupplierAccessGrantedMail(string email, string password, string companyName);
    }
}