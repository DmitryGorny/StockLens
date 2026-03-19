using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockLens.Dtos.AuthDtos;
using StockLens.Dtos.QuotesDtos.Analytics.Responses;
using StockLens.Services.Auth.AuthService;
using System.Security.Claims;

namespace StockLens.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Регистрация пользователя 
        /// </summary>
        /// <response code="200">
        /// Возвращает string оповещающее об отправке письма на почту
        /// </response>
        /// <param name="dto">Логин, почта и пароль</param>
        /// 
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                return Ok(await _authService.Register(dto));
            }
            catch (Exception ex) 
            { 
                return BadRequest(ex.Message);
            }

        }

        /// <summary>
        /// Вход в аккаунт
        /// </summary>
        /// <response code="200">
        /// Возвращает объект с данными пользователями
        /// </response>
        /// <param name="dto">Логин и пароль</param>
        [HttpPost]
        [Route("login")]
        [ProducesResponseType(typeof(NewUserDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var user = await _authService.Login(dto);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        /// <summary>
        /// Обновление JWT токена
        /// </summary>
        /// <param name="CurrentRefreshToken">Текущий refresh token</param>
        [HttpPost]
        [Route("refresh")]
        [ProducesResponseType(typeof(NewUserDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> Refresh([FromBody] string CurrentRefreshToken)
        {

            try
            {
                var user = await _authService.RefreshToken(CurrentRefreshToken);
                if (user == null)
                    return BadRequest();
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        /// <summary>
        /// Подтверждение почты
        /// </summary>
        /// <param name="token">token подтверждения почты (уже сформирован в письме)</param>
        /// <param name="email">подтверждаемый email</param>
        [HttpGet]
        [Route("confirm-email")]
        public async Task<IActionResult> ConfirmEmail([FromQuery] string token, string email)
        {
            try
            {
                var ok = await _authService.ConfirmEmail(email, token);
                return Ok(ok);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
