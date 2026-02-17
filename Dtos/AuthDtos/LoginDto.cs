using System.ComponentModel.DataAnnotations;

namespace StockLens.Dtos.AuthDtos
{
    public class LoginDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
