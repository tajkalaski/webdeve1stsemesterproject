
namespace RespaunceV2.Core.Models
{
    public class QuestionCategoryTranslation
    {
        public string Id { get; set; }
        public QuestionCategory QuestionCategory { get; set; }
        public Language Language { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}