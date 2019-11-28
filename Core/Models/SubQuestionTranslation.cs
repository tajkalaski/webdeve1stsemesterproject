
namespace RespaunceV2.Core.Models
{
    public class SubQuestionTranslation
    {
        public string Id { get; set; }
        public SubQuestion SubQuestion { get; set; }
        public Language Language { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}