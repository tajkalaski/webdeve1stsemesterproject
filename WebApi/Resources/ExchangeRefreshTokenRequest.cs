namespace RespaunceV2.WebApi.Resources
{
    public class ExchangeRefreshTokenRequest
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        //public string SigningKey { get; }
    }
}