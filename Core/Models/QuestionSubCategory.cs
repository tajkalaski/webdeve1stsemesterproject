using System;
using System.Collections.Generic;

namespace RespaunceV2.Core.Models
{
    public class QuestionSubCategory
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Published { get; set; }
        public QuestionCategory QuestionCategory { get; set; }
        public List<Question> Questions { get; set; }
        public List<QuestionSubCategoryTranslation> QuestionSubCategoryTranslations { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
    }
}