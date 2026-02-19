using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using StockLens.Dtos.AuthDtos;
using StockLens.Models;
using StockLens.Services.Auth.EmailSender;
using StockLens.Services.Auth.Token;
using System.Data;
using System.Text;

namespace StockLens.Services.Auth.AuthService
{
    public class AuthService : IAuthService
    {

        private readonly UserManager<User> _userManager;
        private readonly ITokenCreator _tokenService;
        private readonly SignInManager<User> _signinManager;
        private readonly IEmailMessagesSender _emailService;
        public AuthService(UserManager<User> userManager, 
                            ITokenCreator tokenService, 
                            SignInManager<User> signInManager,
                            IEmailMessagesSender emailMessagesSender)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _signinManager = signInManager;
            _emailService = emailMessagesSender;
        }
        public async Task<string> Register(RegisterDto dto)
        {
            var User = new User
            {
                UserName = dto.Username,
                Email = dto.Email,
            };

            var user = await _userManager.FindByEmailAsync(dto.Email);

            if (user != null)
                throw new Exception("Попробуйте другую почту");

            var createUser = await _userManager.CreateAsync(User, dto.Password);

            if (createUser.Succeeded)
            {
                var role = await _userManager.AddToRoleAsync(User, "User");

                if (role.Succeeded)
                {
                    await SendEmailConfirmationAsync(User);
                    return "Письмо с подтверждением было выслано вам на почту";
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

            if (!user.EmailConfirmed)
            {
                await SendEmailConfirmationAsync(user);
                throw new Exception("Почта не подтверждена. Вам было выслано письмо с подтверждением");
            }

            return new NewUserDto
            {
                UserName = user.UserName,
                Email = user.Email,
                Token = _tokenService.CreateJWTToken(user, (List<string>)await _userManager.GetRolesAsync(user)),
                EmailConfirmed = user.EmailConfirmed,
                RefreshToken = await _tokenService.GenerateRefreshToken(user),
            };
        }

        public async Task<string> ConfirmEmail(string recieverEmail, string token)
        {
            var decodedToken = Encoding.UTF8.GetString(
                    WebEncoders.Base64UrlDecode(token)
                );
            var user = await _userManager.FindByEmailAsync(recieverEmail);
            if (user == null)
                throw new UnauthorizedAccessException("Ошибка, попробуйте позже");
            var result = await _userManager.ConfirmEmailAsync(user, decodedToken);

            if (!result.Succeeded)
                throw new UnauthorizedAccessException("Не удалось подтвердить вашу почту");

            return "Почта подтверждена, авторизируйтесь";
        } 

        public async Task<NewUserDto?> RefreshToken(string token, string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
                throw new Exception("Пользователь не найден");

            var result = await _tokenService.CheckRefreshRoken(token, user);

            if (result)
            {
                await _tokenService.SetRevokedRefreshToken(token);
                return new NewUserDto
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    Token = _tokenService.CreateJWTToken(user, (List<string>)await _userManager.GetRolesAsync(user)),
                    EmailConfirmed = user.EmailConfirmed,
                    RefreshToken = await _tokenService.GenerateRefreshToken(user),
                };
            } else
            {
                await _signinManager.SignOutAsync();
                await _tokenService.DeleteUsersTokens(user);
                return null;
            }
        }

        private async Task SendEmailConfirmationAsync(User user)
        {
            var found_user = await _userManager.FindByEmailAsync(user.Email!);

            if (found_user == null)
                throw new Exception($"Пользователя с почтой {user.Email} не существует");

            string token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
            var messageData = $"""  
                    Зравствуйте, {user.UserName}  

                    Для подтверждения адреса перейдите по этой ссылке:
                    http://localhost:5227/api/auth/confirm-email?token={encodedToken}&email={user.Email}   
                    """;

            await _emailService.SendEmailAsync(user.Email, user.UserName, messageData, "Подтверждение адреса электронной почты");
        }
    }
}
