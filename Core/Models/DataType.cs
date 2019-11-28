using System.Collections.Generic;

namespace RespaunceV2.Core.Models
{
    public class SubQuestionDataType
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<SubQuestionDataTypeProperty> DataTypeProperties { get; set; }
    }
}