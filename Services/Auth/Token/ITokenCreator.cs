using StockLens.Models;

namespace StockLens.Services.Auth.Token
{
    public interface ITokenCreator
    {
        public string CreateToken(User user);
    }
}
