using MeusMedicamentos.Application.DTOs.Usuario;
using MeusMedicamentos.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MeusMedicamentos.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutenticacaoController : ControllerBase
    {
        private readonly IAutenticacaoService _autenticacaoService;

        public AutenticacaoController(IAutenticacaoService autenticacaoService)
        {
            _autenticacaoService = autenticacaoService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            var token = await _autenticacaoService.AutenticarAsync(loginDTO.Usuario, loginDTO.Senha);

            if (token == null)
                return Unauthorized();

            return Ok(new { Token = token });
        }

        // Novo endpoint para criar usuário
        [HttpPost("criar-usuario")]
        public async Task<IActionResult> CriarUsuario([FromBody] CriarUsuarioDTO criarUsuarioDTO)
        {
            var result = await _autenticacaoService.CriarUsuarioAsync(criarUsuarioDTO.Usuario, criarUsuarioDTO.Senha, criarUsuarioDTO.Nome, criarUsuarioDTO.Email);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok();
        }
    }
}

