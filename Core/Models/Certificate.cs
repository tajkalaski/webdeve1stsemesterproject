using System.Collections.Generic;

namespace RespaunceV2.Core.Models
{
    public class Certificate : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<CertificateSubRating> CertificateSubRatings { get; set; }
    }
}