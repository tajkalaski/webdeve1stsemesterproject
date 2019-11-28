using System;

namespace RespaunceV2.Core.Models
{
    public class RefreshToken
    {
        public int Id { get; private set; }
        public string Token { get; private set; }
        public DateTime Expires { get; private set; }
        public string ApplicationUserId { get; private set; }
        public bool Active => DateTime.UtcNow <= Expires;
        public string RemoteIpAddress { get; private set; }

        public RefreshToken(string token, DateTime expires, string applicationUserId, string remoteIpAddress)
        {
            Token = token;
            Expires = expires;
            ApplicationUserId = applicationUserId;
            RemoteIpAddress = remoteIpAddress;
        }
    }
}