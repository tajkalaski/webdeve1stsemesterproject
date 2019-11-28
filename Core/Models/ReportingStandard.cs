using System;
using System.Collections.Generic;

namespace RespaunceV2.Core.Models
{
    public class ReportingStandard
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<ReportingStandardQuestion> Question { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
    }
}