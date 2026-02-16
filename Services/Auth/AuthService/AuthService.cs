using Microsoft.AspNetCore.Identity;
using StockLens.Dtos.AuthDtos;
using StockLens.Models;
using StockLens.Services.Auth.Token;
using System.Data;

namespace StockLens.Services.Auth.AuthService
{
    public class AuthService : IAuthService
    {

        private readonly UserManager<User> _userManager;
        private readonly ITokenCreator _tokenService;
        private readonly SignInManager<User> _signinManager;
        public AuthService(UserManager<User> userManager, ITokenCreator tokenService, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _signinManager = signInManager;
        }
        public async Task<NewUserDto> Register(RegisterDto dto)
        {
            var User = new User
            {
                UserName = dto.Username,
                Email = dto.Email,
            };

            var createUser = await _userManager.CreateAsync(User, dto.Password);

            if (createUser.Succeeded)
            {
                var role = await _userManager.AddToRoleAsync(User, "User");

                if (role.Succeeded)
                {
                    return new NewUserDto
                    {
                        UserName = User.UserName,
                        Email = User.Email,
                        Token = _tokenService.CreateToken(User)
                    };
                }
                else throw new Exception(string.Join(", ", role.Errors.Select(e => e.Description)));
            }
            else throw new Exception(string.Join(", ", createUser.Errors.Select(e => e.Description)));
        }

        public async Task<NewUserDto> Login(LoginDto dto)
        {
            var user = await _userManager.FindByNameAsync(dto.Username);

            if (user == null)
                throw new UnauthorizedAccessException("Пользователь не найден");

            var result = await _signinManager.CheckPasswordSignInAsync(user, dto.Password, false);

            if (!result.Succeeded)
                throw new UnauthorizedAccessException("Неверный пароль");

            return new NewUserDto
            {
                UserName = user.UserName,
                Email = user.Email,
                Token = _tokenService.CreateToken(user)
            };
        }
    }
}
