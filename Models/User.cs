using Microsoft.AspNetCore.Identity;

namespace StockLens.Models
{
    public class User : IdentityUser
    {
        public List<RefreshTokens> Refreshtokens { get; set; }
    }
} 
