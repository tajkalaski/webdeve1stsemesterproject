using System.Collections.Generic;

namespace RespaunceV2.Core.Models
{
    public class ReportingStandardQuestion
    {
        public string Id { get; set; }
        public string QuestionId { get; set; }
        public Question Question { get; set; }
        public string ReportingStandardId { get; set; }
        public ReportingStandard ReportingStandard { get; set; }
        
    }
}