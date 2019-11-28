namespace RespaunceV2.Core.Models
{
    public class KPI : BaseEntity
    {
        public string Name { get; set; }
        public string DataEntry1 { get; set; }
        public string DataEntry2 { get; set; }
        public string Operation { get; set; }
        public int Order { get; set; }
    }
}