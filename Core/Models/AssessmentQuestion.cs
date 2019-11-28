namespace RespaunceV2.Core.Models
{
    public class AssessmentQuestion : BaseEntity
    {
        public Assessment Assessment { get; set; }
        public string AssessmentId { get; set; }
        public SubQuestion SubQuestion { get; set; }
        public string SubQuestionId { get; set; }
        public ResponsiblePerson ResponsiblePerson { get; set; }
        public string ResponsiblePersonId { get; set; }
        
    }
}