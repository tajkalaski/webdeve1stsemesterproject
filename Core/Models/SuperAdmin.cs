using Microsoft.AspNetCore.Identity;

namespace RespaunceV2.Core.Models
{
    public class SuperAdmin : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmployeeNo { get; set; }

    }
}