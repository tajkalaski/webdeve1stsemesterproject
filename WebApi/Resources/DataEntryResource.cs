using System;

namespace RespaunceV2.WebApi.Resources
{
    public class DataEntryResource
    {
        public string Id { get; set; }
        public string SubQuestionId { get; set; }
        public DataEntrySubQuestionResource SubQuestion { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public string Value { get; set; }
        public string AddedBy { get; set; }
    }
}