namespace RespaunceV2.WebApi.Resources
{
    public class AssessmentQuestionResource
    {
        public string Id { get; set; }
        public string AssessmentId { get; set; }
        public string SubQuestionId { get; set; }
        public UserResource ResponsiblePerson { get; set; }
    }
}