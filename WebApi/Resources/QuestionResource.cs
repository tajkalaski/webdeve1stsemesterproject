using System.Collections.Generic;

namespace RespaunceV2.WebApi.Resources
{
    public class QuestionResource
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Expanded { get; set; }
        public List<SubQuestionResource> SubQuestions { get; set; }

    }
}