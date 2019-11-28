using System.Collections.Generic;

namespace RespaunceV2.Core.Models
{
    public class CompanyCertificateSubRating : BaseEntity
    {
        public string Score { get; set; }
        public CompanyCertificate CompanyCertificate { get; set; }
        public CertificateSubRating CertificateSubRating { get; set; }
        public List<CompanyCertificateSubRatingAction> CertificateSubRatingActions { get; set; }
    }
}