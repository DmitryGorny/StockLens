using StockLens.Models;

namespace StockLens.Services.Auth.Token
{
    public interface ITokenCreator
    {
        public string CreateJWTToken(User user, List<string> roles);
        public Task<string> GenerateRefreshToken(User user);
        public Task<bool> CheckRefreshRoken(string token, User user);
        public Task SetRevokedRefreshToken(string token);
        public Task DeleteUsersTokens(User user);
    }
}
