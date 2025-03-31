using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Models.Entidades;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace TODO.Helpers
{
    public class TokenHelper
    {
        private readonly IConfiguration _config;
        public TokenHelper(IConfiguration config)
        {
            _config = config;
        }

        // metodo que genera json web token del usuario. con el id y su correo.
        public string TokenJwt(Usuario modelo)
        {
            var keySegura = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]!));
            var credenciales = new SigningCredentials(keySegura,SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim("sub", modelo.Id.ToString()),
                new Claim(ClaimTypes.Email, modelo.Correo!)
            };


            var token = new JwtSecurityToken
            (
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: credenciales
            );

            var data = new JwtSecurityTokenHandler().WriteToken(token);

            return data.ToString();
        }
    }
}
