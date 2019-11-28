using System;
using System.Collections.Generic;

namespace RespaunceV2.Core.Models
{
    public class DataEntry: BaseEntity
    {
        public SubQuestion Subquestion { get; set; }
        public Worksite Worksite { get; set; }
        public DateTime To { get; set; }
        public DateTime From { get; set; }
        public double Value{ get; set; }
        
    }
}