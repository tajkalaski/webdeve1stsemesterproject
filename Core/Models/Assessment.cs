using System.Collections.Generic;

namespace RespaunceV2.Core.Models
{
    public class Assessment : BaseEntity
    {
        public string Title { get; set; }
        public Company Company { get; set; }
        public string CompanyId { get; set; }
        public List<AssessmentQuestion> AssessmentQuestions { get; set; }
    }
}