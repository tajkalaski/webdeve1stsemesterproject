using System;
using System.Collections.Generic;

namespace RespaunceV2.Core.Models
{
    public class SubQuestion
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Published { get; set; }
        public Question Question { get; set; }
        public List<DataEntry> QuestionData { get; set; }
        public List<AssessmentQuestion> AssessmentQuestions { get; set; }
        public List<SubQuestionTranslation> SubQuestionTranslations { get; set; }
        //public List<DataEntry> SubQuestionUnits { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
    }
}