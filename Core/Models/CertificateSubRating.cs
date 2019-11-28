namespace RespaunceV2.Core.Models
{
    public class CertificateSubRating : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Certificate Certificate { get; set; }

    }
}