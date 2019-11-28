using System.Collections.Generic;

namespace RespaunceV2.WebApi.Resources
{
    public class AssessmentResource
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string CompanyId { get; set; }
        public List<AssessmentQuestionResource> AssessmentQuestions { get; set; }
    }
}