namespace StockLens.Dtos.AuthDtos
{
    public class NewUserDto : UsersСharacteristicsDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public bool EmailConfirmed { get; set; }
        public string Role { get; set; }
    }
}
