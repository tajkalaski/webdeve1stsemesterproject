using System;
using System.Collections.Generic;

namespace RespaunceV2.Core.Models
{
    public class Question
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Published { get; set; }
        public QuestionSubCategory QuestionSubCategory { get; set; }
        public List<SubQuestion> SubQuestions { get; set; }
        public List<QuestionTranslation> QuestionTranslations { get; set; }
        public List<ReportingStandardQuestion> ReportingStandard { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
    }
}