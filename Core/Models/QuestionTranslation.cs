
namespace RespaunceV2.Core.Models
{
    public class QuestionTranslation
    {
        public string Id { get; set; }
        public Question Question { get; set; }
        public Language Language { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}