using System;
using System.Collections.Generic;

namespace RespaunceV2.Core.Models
{
    public class FocusArea : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Strategi { get; set; }
        public string Risk { get; set; }
        public ApplicationUser Responsible { get; set; }
        public ApplicationUser Owner { get; set; }
        public List<UNGCAspect> UNGCAspects { get; set; }
        public List<FocusAreaInitiative> FocusAreaInitiatives { get; set; }
        public List<KPI> KPIs { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}