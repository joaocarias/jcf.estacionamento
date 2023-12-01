using Jcf.Estacionamento.Api.Models;
using Jcf.Estacionamento.Api.Servicos.IServicos;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Jcf.Estacionamento.Api.Servicos
{
    public class TokenServico : ITokenServico
    {
        private readonly IConfiguration _configuration;

        public TokenServico(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public ClaimsIdentity GeradorClaims(Usuario usuario)
        {
            var cli = new ClaimsIdentity(new[]
                 {
                    new Claim("USER_ID", usuario.Id.ToString()),
                    new Claim(ClaimTypes.Name, usuario.Email),
                    new Claim(ClaimTypes.Role, usuario.Role.ToUpper()),
                    new Claim("USER_NAME", usuario.Nome)
                });

            return cli;
        }

        public string NovoToken(Usuario usuario)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Authentication:Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = GeradorClaims(usuario),
                Expires = DateTime.UtcNow.AddHours(8),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),

                Issuer = _configuration["Authentication:Jwt:Issuer"],
                Audience = _configuration["Authentication:Jwt:Audience"]
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
