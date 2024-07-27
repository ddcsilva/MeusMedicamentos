using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MeusMedicamentos.Application.DTOs.Usuario;
using MeusMedicamentos.Application.Interfaces;
using MeusMedicamentos.Domain.Entities;
using MeusMedicamentos.Shared;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Logging;

namespace MeusMedicamentos.Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly ILogger<UsuarioService> _logger;

        public UsuarioService(UserManager<Usuario> userManager, SignInManager<Usuario> signInManager, IConfiguration configuration, ILogger<UsuarioService> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<ApiResponse<IEnumerable<UsuarioDTO>>> ObterTodosUsuariosAsync()
        {
            var usuarios = _userManager.Users.ToList();
            var usuarioDTOs = usuarios.Select(u => new UsuarioDTO(u.Id, u.Nome, u.Email)).ToList();
            return new ApiResponse<IEnumerable<UsuarioDTO>>(usuarioDTOs, 200);
        }

        public async Task<ApiResponse<UsuarioDTO>> ObterPorIdAsync(Guid id)
        {
            var usuario = await _userManager.FindByIdAsync(id.ToString());
            if (usuario == null)
            {
                return new ApiResponse<UsuarioDTO>("Usuário não encontrado", 404);
            }

            var usuarioDTO = new UsuarioDTO(usuario.Id, usuario.Nome, usuario.Email);
            return new ApiResponse<UsuarioDTO>(usuarioDTO, 200);
        }

        public async Task<IdentityResult> CriarUsuarioAsync(string userName, string senha, string nome, string email)
        {
            var user = new Usuario
            {
                UserName = userName,
                Nome = nome,
                Email = email
            };

            var result = await _userManager.CreateAsync(user, senha);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Usuario");
            }

            return result;
        }

        public async Task<IdentityResult> AtualizarUsuarioAsync(Guid id, string userName, string nome, string email)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "Usuário não encontrado" });
            }

            user.UserName = userName;
            user.Nome = nome;
            user.Email = email;

            return await _userManager.UpdateAsync(user);
        }

        public async Task<IdentityResult> RemoverUsuarioAsync(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "Usuário não encontrado" });
            }

            return await _userManager.DeleteAsync(user);
        }

        public async Task<string?> AutenticarAsync(string userName, string senha)
        {
            _logger.LogInformation($"Autenticando usuário: {userName}");

            var user = await _userManager.FindByNameAsync(userName);
            if (user != null)
            {
                var result = await _signInManager.CheckPasswordSignInAsync(user, senha, false);
                if (result.Succeeded)
                {
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

                    _logger.LogInformation($"Gerando token para o usuário: {userName}");

                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new[]
                        {
                            new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName ?? string.Empty),
                            new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString())
                        }),
                        Expires = DateTime.UtcNow.AddMinutes(expiryMinutes),
                        Issuer = issuer,
                        Audience = audience,
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                    };

                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    var tokenString = tokenHandler.WriteToken(token);

                    _logger.LogInformation($"Token gerado: {tokenString}");

                    return tokenString;
                }
                else
                {
                    _logger.LogWarning($"Falha na autenticação do usuário: {userName}");
                }
            }
            else
            {
                _logger.LogWarning($"Usuário não encontrado: {userName}");
            }

            return null;
        }
    }
}
