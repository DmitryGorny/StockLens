namespace StockLens.Dtos.AuthDtos
{
    public class NewUserDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public bool EmailConfirmed { get; set; }
    }
}
