using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace RespaunceV2.Core.Models
{
    public class Division : BaseEntity{


        public Company Company { get; set;}
        public string Name { get; set; }
        public List<Worksite> Worksites { get; set; }
        
    }
}