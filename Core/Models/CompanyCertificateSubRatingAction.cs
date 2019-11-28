using System;

namespace RespaunceV2.Core.Models
{
    public class CompanyCertificateSubRatingAction : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public bool Approved { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }
}