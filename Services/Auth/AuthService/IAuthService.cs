using StockLens.Dtos.AuthDtos;
using StockLens.Models;

namespace StockLens.Services.Auth.AuthService
{
    public interface IAuthService
    {
        public Task<string> Register(RegisterDto dto);
        public Task<NewUserDto> Login(LoginDto dto);
        public Task<string> ConfirmEmail(string recieverEmail, string token);
        public Task<NewUserDto?> RefreshToken(string token);
        public Task<UsersСharacteristicsDto> GetUsersMetrics(string email);
    }
}
