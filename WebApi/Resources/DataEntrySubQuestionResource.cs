namespace RespaunceV2.WebApi.Resources
{
    public class DataEntrySubQuestionResource
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DataEntryQuestionResource Question { get; set; }
    }
}