using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace RespaunceV2.Core.Models
{
    public class Company
    {
        public string Id { get; set; }
        public Company Parent { get; set; }
        public string Name { get; set; }
        public string VATIN { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public bool Public { get; set; }
        public string FinancialYearStart { get; set; }
        public string FinancialYearEnd { get; set; }
        public int? Employees { get; set; }
        public float? AnnualRevenue { get; set; }
        public string TypeOfOwnershipId { get; set; }
        public TypeOfOwnership TypeOfOwnership { get; set; }
        public string LegalFormId { get; set; }
        public LegalForm LegalForm { get; set; }
        public string Logo { get; set; }
        [NotMapped]
        public List<CompanySupplier> Suppliers { get; set; }
        public List<CompanyCertificate> CompanyCertificates { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
    }
}