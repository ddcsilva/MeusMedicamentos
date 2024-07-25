using MeusMedicamentos.Application.DTOs;
using MeusMedicamentos.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MeusMedicamentos.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriasController : ControllerBase
{
    private readonly ICategoriaService _categoriaService;

    public CategoriasController(ICategoriaService categoriaService)
    {
        _categoriaService = categoriaService;
    }

    // GET: api/categorias
    [HttpGet]
    public async Task<IActionResult> ObterTodos()
    {
        var response = await _categoriaService.ObterTodosAsync();
        return response.Success ? Ok(response) : BadRequest(response);
    }

    // GET: api/categorias/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> ObterPorId(int id)
    {
        var response = await _categoriaService.ObterPorIdAsync(id);
        return response.Success ? Ok(response) : NotFound(response);
    }

    // POST: api/categorias
    [HttpPost]
    public async Task<IActionResult> Adicionar([FromBody] CriarCategoriaDTO categoriaDTO)
    {
        var response = await _categoriaService.AdicionarAsync(categoriaDTO);
        return response.Success ? CreatedAtAction(nameof(ObterPorId), new { id = response.Data.Id }, response) : BadRequest(response);
    }

    // PUT: api/categorias/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> Atualizar(int id, [FromBody] EditarCategoriaDTO categoriaDTO)
    {
        categoriaDTO.Id = id;
        var response = await _categoriaService.AtualizarAsync(categoriaDTO);
        return response.Success ? Ok(response) : BadRequest(response);
    }

    // DELETE: api/categorias/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> Remover(int id)
    {
        var response = await _categoriaService.RemoverAsync(id);
        return response.Success ? Ok(response) : NotFound(response);
    }
}