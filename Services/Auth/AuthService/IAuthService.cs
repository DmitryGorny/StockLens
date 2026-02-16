using StockLens.Dtos.AuthDtos;

namespace StockLens.Services.Auth.AuthService
{
    public interface IAuthService
    {
        public Task<NewUserDto> Register(RegisterDto dto);

        public Task<NewUserDto> Login(LoginDto dto);
    }
}
