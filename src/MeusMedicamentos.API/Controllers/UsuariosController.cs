using MeusMedicamentos.Application.DTOs.Usuario;
using MeusMedicamentos.Application.Interfaces;
using MeusMedicamentos.Domain.Notifications;
using MeusMedicamentos.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MeusMedicamentos.API.Controllers;

[Authorize]
[Route("api/[controller]")]
public class UsuariosController : MainController
{
    private readonly IUsuarioService _usuarioService;

    public UsuariosController(IUsuarioService usuarioService, INotificadorErros notificadorErros)
        : base(notificadorErros)
    {
        _usuarioService = usuarioService;
    }

    [HttpGet]
    public async Task<IActionResult> ObterTodos()
    {
        var response = await _usuarioService.ObterTodosUsuariosAsync();
        return CustomResponse(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> ObterPorId(Guid id)
    {
        var response = await _usuarioService.ObterPorIdAsync(id);
        return CustomResponse(response);
    }

    [HttpPost]
    public async Task<IActionResult> Adicionar([FromBody] CriarUsuarioDTO usuarioDTO)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        var response = await _usuarioService.CriarUsuarioAsync(usuarioDTO.UserName, usuarioDTO.Nome, usuarioDTO.Email);
        return CustomResponse(response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Atualizar(Guid id, [FromBody] EditarUsuarioDTO usuarioDTO)
    {
        if (id != usuarioDTO.Id)
        {
            NotificarErro("Erro ao atualizar o usuário: Id da requisição difere do Id do objeto");
            return CustomResponse(new ApiResponse<string>("Erro ao atualizar o usuário: Id da requisição difere do Id do objeto", 400));
        }

        if (!ModelState.IsValid) return CustomResponse(ModelState);

        var result = await _usuarioService.AtualizarUsuarioAsync(id, usuarioDTO.UserName, usuarioDTO.Nome, usuarioDTO.Email);
        return CustomResponse(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Remover(Guid id)
    {
        var result = await _usuarioService.RemoverUsuarioAsync(id);
        return CustomResponse(result);
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
    {
        var token = await _usuarioService.AutenticarAsync(loginDTO.UserName, loginDTO.Senha);

        if (token == null)
            return Unauthorized();

        return Ok(new { Token = token });
    }
}
