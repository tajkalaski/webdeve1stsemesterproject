using System;

namespace RespaunceV2.Core.Models
{
    public class LegalForm
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
    }
}