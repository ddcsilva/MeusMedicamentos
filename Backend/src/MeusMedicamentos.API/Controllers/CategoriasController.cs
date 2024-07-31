using MeusMedicamentos.Application.DTOs;
using MeusMedicamentos.Application.Interfaces;
using MeusMedicamentos.Domain.Notifications;
using MeusMedicamentos.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MeusMedicamentos.API.Controllers;

// [Authorize]
[Route("api/[controller]")]
public class CategoriasController : MainController
{
    private readonly ICategoriaService _categoriaService;

    public CategoriasController(ICategoriaService categoriaService, INotificadorErros notificadorErros)
        : base(notificadorErros)
    {
        _categoriaService = categoriaService;
    }

    [HttpGet]
    public async Task<IActionResult> ObterTodos()
    {
        var response = await _categoriaService.ObterTodosAsync();
        return CustomResponse(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> ObterPorId(Guid id)
    {
        var response = await _categoriaService.ObterPorIdAsync(id);
        return CustomResponse(response);
    }

    [HttpPost]
    public async Task<IActionResult> Adicionar([FromBody] CriarCategoriaDTO categoriaDTO)
    {
        if (!ModelState.IsValid)
        {
            // Log do model state inválido
            foreach (var value in ModelState.Values)
            {
                foreach (var error in value.Errors)
                {
                    Console.WriteLine($"Error: {error.ErrorMessage}");
                }
            }
            return CustomResponse(ModelState);
        }

        var response = await _categoriaService.AdicionarAsync(categoriaDTO);
        return CustomResponse(response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Atualizar(Guid id, [FromBody] EditarCategoriaDTO categoriaDTO)
    {
        if (id != categoriaDTO.Id)
        {
            return CustomResponse(new ApiResponse<string>("Erro ao atualizar a categoria: Id da requisição difere do Id do objeto", 400));
        }

        if (!ModelState.IsValid) return CustomResponse(ModelState);

        var response = await _categoriaService.AtualizarAsync(categoriaDTO);
        return CustomResponse(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Remover(Guid id)
    {
        var response = await _categoriaService.RemoverAsync(id);
        return CustomResponse(response);
    }
}
