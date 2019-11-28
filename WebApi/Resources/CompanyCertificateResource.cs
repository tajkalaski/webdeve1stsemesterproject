using System;

namespace RespaunceV2.WebApi.Resources
{
    public class CompanyCertificateResource
    {
        public string Id { get; set; }
        public string CertificateName { get; set; }
        public DateTime CertifiedFrom { get; set; }
        public DateTime CertifiedTo { get; set; }
        public string OverallRating { get; set; }
    }
}