using System;
using System.Collections.Generic;

namespace RespaunceV2.Core.Models
{
    public class QuestionCategory
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Published { get; set; }
        public List<QuestionSubCategory> QuestionSubCategories { get; set; }
        public List<QuestionCategoryTranslation> QuestionCategoryTranslations { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
    }
}