namespace StockLens.Repositories.RefreshTokens
{
    public interface IRefreshTokensRepository
    {
        public Task CreateToken(Models.RefreshTokens token);
        public Task UpdateToken(Models.RefreshTokens token);
        public Task<Models.RefreshTokens?> GetToken(int tokenId);
        public Task<Models.RefreshTokens?> GetToken(string token);
        public Task<Models.RefreshTokens?> GetTokenJoinUser(string token);
        public Task DeleteTokensAsync(string userId);
    }
}
