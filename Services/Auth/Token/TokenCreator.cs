using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StockLens.Models;
using StockLens.Repositories.RefreshTokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace StockLens.Services.Auth.Token
{
    public class TokenCreator : ITokenCreator
    {
        private readonly IConfiguration _config;
        private readonly SymmetricSecurityKey _key;
        private readonly IRefreshTokensRepository _refreshTokenRepository;

        public TokenCreator(IConfiguration config, IRefreshTokensRepository refreshTokensRepository)
        {
            _config = config;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:SigningKey"]));
            _refreshTokenRepository = refreshTokensRepository;
        }

        public string CreateJWTToken(User user, List<string> roles)
        {
            
            List<Claim> claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.GivenName, user.UserName),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
            };

            foreach (var r in roles) 
            {
                claims.Add(new Claim(ClaimTypes.Role, r));
            }

            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Issuer = _config["JWT:Issuer"],
                Expires = DateTime.Now.AddHours(1),
                Audience = _config["JWT:Audience"],
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor); 

            return tokenHandler.WriteToken(token);
        }

        public async Task<string> GenerateRefreshToken(User user)
        {
            string token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));

            var tokenEnt = new RefreshTokens
            {
                User = user,
                UserId = user.Id,
                Token = token,
                ExiresOn = DateTime.UtcNow.AddDays(7)
            };

            await _refreshTokenRepository.CreateToken(tokenEnt);

            return token;
        }

        public async Task<bool> CheckRefreshRoken(string token, User user)
        {
            RefreshTokens? tokenEnt = await _refreshTokenRepository.GetToken(token);
            if (tokenEnt == null)
                throw new Exception("RefreshToken невалиден");

            if (tokenEnt.isRevoked)
                return false;

            if (tokenEnt.UserId != user.Id)
                return false;

            if (tokenEnt.ExiresOn <= DateTime.UtcNow) 
                return false;

            return true;
        }

        public async Task SetRevokedRefreshToken(string token)
        {
            var tokenEnt = await _refreshTokenRepository?.GetToken(token);

            if (tokenEnt == null)
                throw new Exception("Токен не был найден");

            tokenEnt.isRevoked = true;
            
            await _refreshTokenRepository.UpdateToken(tokenEnt);
        }

        public async Task DeleteUsersTokens(User user)
        {
            await _refreshTokenRepository.DeleteTokensAsync(user.Id);
        }
    }
}
