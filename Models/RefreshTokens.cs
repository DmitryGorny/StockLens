namespace StockLens.Models
{
    public class RefreshTokens
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public DateTime ExiresOn { get; set; }
        public string UserId { get; set; }
        public bool isRevoked { get; set; }
        public int ChangedWithId { get; set; }
        public User User { get; set; }
    }
}
