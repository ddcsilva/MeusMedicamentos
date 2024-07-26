using MeusMedicamentos.Application.Interfaces;
using MeusMedicamentos.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MeusMedicamentos.Application.Services
{
    public class AutenticacaoService : IAutenticacaoService
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;
        private readonly IConfiguration _configuration;

        public AutenticacaoService(UserManager<Usuario> userManager, SignInManager<Usuario> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        public async Task<string> AutenticarAsync(string usuario, string senha)
        {
            // Valida as credenciais do usuário
            var user = await _userManager.FindByNameAsync(usuario);
            if (user != null)
            {
                var result = await _signInManager.CheckPasswordSignInAsync(user, senha, false);
                if (result.Succeeded)
                {
                    // Gerar o token JWT
                    var jwtSettings = _configuration.GetSection("JwtSettings");
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var key = Encoding.UTF8.GetBytes(jwtSettings["Secret"]);
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new[]
                        {
                            new Claim(ClaimTypes.Name, user.UserName),
                            new Claim(ClaimTypes.NameIdentifier, user.Id)
                        }),
                        Expires = DateTime.UtcNow.AddMinutes(Convert.ToInt32(jwtSettings["ExpiryMinutes"])),
                        Issuer = jwtSettings["Issuer"],
                        Audience = jwtSettings["Audience"],
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                    };
                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    return tokenHandler.WriteToken(token);
                }
            }

            return null;
        }
    }
}
