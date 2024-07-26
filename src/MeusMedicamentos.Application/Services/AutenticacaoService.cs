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

        public async Task<string?> AutenticarAsync(string usuario, string senha)
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
                    var secret = jwtSettings["Secret"];
                    var issuer = jwtSettings["Issuer"];
                    var audience = jwtSettings["Audience"];
                    var expiryMinutesStr = jwtSettings["ExpiryMinutes"];

                    if (string.IsNullOrEmpty(secret) || string.IsNullOrEmpty(issuer) || string.IsNullOrEmpty(audience) || !int.TryParse(expiryMinutesStr, out var expiryMinutes))
                    {
                        throw new InvalidOperationException("JWT settings are not configured properly.");
                    }

                    var tokenHandler = new JwtSecurityTokenHandler();
                    var key = Encoding.UTF8.GetBytes(secret);
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new[]
                        {
                            new Claim(ClaimTypes.Name, user.UserName ?? string.Empty),
                            new Claim(ClaimTypes.NameIdentifier, user.Id ?? string.Empty)
                        }),
                        Expires = DateTime.UtcNow.AddMinutes(expiryMinutes),
                        Issuer = issuer,
                        Audience = audience,
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
