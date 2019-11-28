using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace RespaunceV2.Core.Models
{
    public class BaseEntity
    {
        public string Id { get; set; }
        public DateTime CreatedOn { get; set; }
        [Column("CreatedBy")]
        public string CreatedById { get; set; }
        public ApplicationUser CreatedBy { get; set; }
    }
}