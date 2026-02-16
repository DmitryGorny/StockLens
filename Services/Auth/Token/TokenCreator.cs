using Microsoft.IdentityModel.Tokens;
using StockLens.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StockLens.Services.Auth.Token
{
    public class TokenCreator : ITokenCreator
    {
        private readonly IConfiguration _config;
        private readonly SymmetricSecurityKey _key;

        public TokenCreator(IConfiguration config)
        {
            _config = config;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:SigningKey"]));
        }

        public string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.GivenName, user.UserName),
                new Claim(JwtRegisteredClaimNames.Email, user.Email)
            };

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
    }
}
