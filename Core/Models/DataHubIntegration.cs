
using System;

namespace RespaunceV2.Core.Models
{
    public class DataHubIntegration : BaseEntity
    {
        public string DataHubAuthorizationId { get; set; }
        public string DataHubAuthorizationGuid { get; set; }
        public string ThirdPartyKey { get; set; }
        public bool HistoricData { get; set; }
        public bool DeleteExistingAuthorizations { get; set; }
        public string SignedBy { get; set; }
        public DateTime? SignedTimeStamp { get; set; }
        public DateTime? DeleteDate { get; set; }
        public Company Company { get; set; }
        public DateTime AuthorizedFrom { get; set; }
    }
}