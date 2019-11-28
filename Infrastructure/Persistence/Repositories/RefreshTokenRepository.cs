using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RespaunceV2.Core.Interfaces;
using RespaunceV2.Core.Models;

namespace RespaunceV2.Infrastructure.Persistence.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly ITokenFactory _tokenFactory;
        private readonly ReportingDbContext _context;
        public RefreshTokenRepository(ITokenFactory tokenFacotry, ReportingDbContext context)
        {
            _context = context;
            _tokenFactory = tokenFacotry;
        }
        public async Task<string> CreateToken(string id, string remoteIpAddress)
        {
            var token =_tokenFactory.GenerateToken();
            var refreshToken = new RefreshToken(token, DateTime.UtcNow.AddDays(5), id, remoteIpAddress);
            await _context.RefreshTokens.AddAsync(refreshToken);
            return token;
        }

        public void Delete(RefreshToken refreshToken)
        {
            _context.RefreshTokens.Remove(refreshToken);
        }

        public async Task<RefreshToken> GetByToken(string token)
        {
            return await _context.RefreshTokens.SingleOrDefaultAsync(rt => rt.Token == token);
        }

        public async Task<RefreshToken> GetByUserId(string id)
        {
            return await _context.RefreshTokens.SingleOrDefaultAsync(rt => rt.ApplicationUserId == id);
        }
    }
}