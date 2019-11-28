using System;

namespace RespaunceV2.WebApi.Resources
{
    public class SaveDataEntryResource
    {
        
        public string WorksiteId { get; set; }
        public string SubQuestionId { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public double Value { get; set; }
        public string UserId { get; set; }
    }
}