namespace RespaunceV2.WebApi.Resources
{
    public class ManageCompanyResource
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string VATIN { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public bool Public { get; set; }
        public string FinancialYearStart { get; set; }
        public string FinancialYearEnd { get; set; }
        public int? Employees { get; set; }
        public int? AnnualRevenue { get; set; }
        public string TypeOfOwnershipId { get; set; }
        public string LegalFormId { get; set; }
        public string Logo { get; set; }
    }
}