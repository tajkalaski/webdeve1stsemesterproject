using System.Collections.Generic;

namespace RespaunceV2.WebApi.Resources
{
    public class QuestionCategoryResource
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Expanded { get; set; }
        public List<QuestionSubCategoryResource> QuestionSubCategories { get; set; }
    }
}