
namespace RespaunceV2.Core.Models
{
    public class QuestionSubCategoryTranslation
    {
        public string Id { get; set; }
        public QuestionSubCategory QuestionSubCategory { get; set; }
        public Language Language { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}