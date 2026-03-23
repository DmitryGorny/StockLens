using System.ComponentModel.DataAnnotations;

namespace StockLens.Dtos.AuthDtos
{
    public class RegisterDto : UsersСharacteristicsDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }
    }
}
