using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace RespaunceV2.Core.Models
{
    public class ResponsiblePerson : BaseEntity
    {
        public ApplicationUser ApplicationUser { get; set; }
        public string ApplicationUserId { get; set; }
        public List<AssessmentQuestion> AssessmentQuestions { get; set; }
    }
}