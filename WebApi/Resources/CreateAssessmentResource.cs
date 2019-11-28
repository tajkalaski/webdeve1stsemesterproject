using System.Collections.Generic;

namespace RespaunceV2.WebApi.Resources
{
    public class CreateAssessmentResource
    {
        public string Title { get; set; }
        public string CompanyId { get; set; }
        public List<AssessmentQuestionResource> AssessmentQuestions { get; set; }
    }
}