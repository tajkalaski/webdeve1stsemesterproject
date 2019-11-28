using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace RespaunceV2.Core.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Company Company { get; set; }
        public string CompanyId { get; set; }
        public Language Language { get; set; }
        public string LanguageId { get; set; }
        public Role Role { get; set; }
        public string RoleId { get; set; }
        public List<RefreshToken> RefreshTokens { get; set; }
        public List<Assessment> Assessments { get; set; }
        public List<AssessmentQuestion> AssessmentQuestions { get; set; }
        [NotMapped]
        public List<ResponsiblePerson> ResponsiblePersons { get; set; }
        public List<DataEntry> QuestionData { get; set; }
        public DateTime CreatedOn { get; set; }
        public ApplicationUser CreatedBy { get; set; }
    }
}