using Microsoft.EntityFrameworkCore;
using RTools_NTS.Util;
using StockLens.data;
using System.Threading.RateLimiting;

namespace StockLens.Repositories.RefreshTokens
{
    public class RefreshTokensRepository : IRefreshTokensRepository
    {
        private readonly AppDBContext _dbContext;

        public RefreshTokensRepository(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task UpdateToken(Models.RefreshTokens token)
        {
            _dbContext.RefreshTokens.Update(token);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<Models.RefreshTokens?> GetToken(int tokenId)
        {
            return await _dbContext.RefreshTokens.FirstOrDefaultAsync(t => t.Id == tokenId);
        }
        public async Task<Models.RefreshTokens?> GetToken(string token)
        {
            return await _dbContext.RefreshTokens.FirstOrDefaultAsync(t => t.Token == token);
        }

        public async Task CreateToken(Models.RefreshTokens token)
        {
            if (token == null || token.ExiresOn < DateTime.UtcNow)
                throw new Exception("Токен невалиден");

            _dbContext.RefreshTokens.Add(token);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteTokensAsync(string userId)
        {
            List<Models.RefreshTokens> tokens = _dbContext.RefreshTokens.Where(t => t.UserId == userId).ToList();
            _dbContext.RefreshTokens.RemoveRange(tokens);
            await _dbContext.SaveChangesAsync();
        }
    }
}
