using System;
using System.Collections.Generic;

namespace RespaunceV2.Core.Models
{
    public class CompanyCertificate : BaseEntity
    {
        public string CompanyId { get; set; }
        public Company Company { get; set; }
        public string CertificateId { get; set; }
        public Certificate Certificate { get; set; }
        public string OverallRating { get; set; }
        public List<CompanyCertificateSubRating> CompanyCertificateSubRatings { get; set; }
        public DateTime CertifiedFrom { get; set; }
        public DateTime CertifiedTo { get; set; }
    }
}