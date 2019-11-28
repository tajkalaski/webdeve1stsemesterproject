using System.ComponentModel.DataAnnotations.Schema;

namespace RespaunceV2.Core.Models
{
    public class CompanySupplier
    {
        public string CompanyId { get; set; }
        public Company Company { get; set; }
        public string SupplierId { get; set; }
        public Company Supplier { get; set; }
    }
}