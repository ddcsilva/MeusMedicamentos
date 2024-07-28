using MeusMedicamentos.Application.DTOs;
using MeusMedicamentos.Application.Interfaces;
using MeusMedicamentos.Domain.Notifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MeusMedicamentos.Web.Controllers;

[Authorize]
public class CategoriasController : MainController
{
    private readonly ICategoriaService _categoriaService;

    public CategoriasController(ICategoriaService categoriaService, INotificadorErros notificadorErros)
        : base(notificadorErros)
    {
        _categoriaService = categoriaService;
    }

    public async Task<IActionResult> Index()
    {
        var response = await _categoriaService.ObterTodosAsync();
        return CustomResponse(response);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CriarCategoriaDTO categoriaDTO)
    {
        if (!ModelState.IsValid)
        {
            NotificarErroModelInvalida();
            return View(categoriaDTO);
        }

        var response = await _categoriaService.AdicionarAsync(categoriaDTO);
        return CustomResponse(response, categoriaDTO);
    }

    public async Task<IActionResult> Edit(Guid id)
    {
        var response = await _categoriaService.ObterPorIdAsync(id);
        if (response.Success && response.Data != null)
        {
            var categoriaDTO = new EditarCategoriaDTO(
                response.Data.Id,
                response.Data.Nome,
                response.Data.Status
            );
            return View(categoriaDTO);
        }
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(EditarCategoriaDTO categoriaDTO)
    {
        if (!ModelState.IsValid)
        {
            NotificarErroModelInvalida();
            return View(categoriaDTO);
        }

        var response = await _categoriaService.AtualizarAsync(categoriaDTO);
        return CustomResponse(response, categoriaDTO);
    }

    public async Task<IActionResult> Delete(Guid id)
    {
        var response = await _categoriaService.ObterPorIdAsync(id);
        return CustomResponse(response);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        var response = await _categoriaService.RemoverAsync(id);
        return CustomResponse(response);
    }
}
