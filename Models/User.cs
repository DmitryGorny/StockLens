using Microsoft.AspNetCore.Identity;

namespace StockLens.Models
{
    public class User : IdentityUser
    {
        public List<RefreshTokens> Refreshtokens { get; set; }
        public List<Briefcases> UsersBriefcases { get; set; }
        public int ReactionToDrop { get; set; }
        public int MaxDrawdownPercent { get; set; }
        public int InvestmentHorizon { get; set; }
        public int Experience { get; set; }
    }
} 
