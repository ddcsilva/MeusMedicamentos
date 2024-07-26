using MeusMedicamentos.Application.DTOs;
using MeusMedicamentos.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MeusMedicamentos.Web.Controllers
{
    [Authorize]
    public class CategoriasController : Controller
    {
        private readonly ICategoriaService _categoriaService;

        public CategoriasController(ICategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        public async Task<IActionResult> Index()
        {
            var response = await _categoriaService.ObterTodosAsync();
            if (response.Success)
            {
                return View(response.Data);
            }
            else
            {
                return View(new List<CategoriaDTO>());
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CriarCategoriaDTO categoriaDTO)
        {
            if (ModelState.IsValid)
            {
                var response = await _categoriaService.AdicionarAsync(categoriaDTO);
                if (response.Success)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    var firstError = response.Errors?.FirstOrDefault();
                    if (firstError != null)
                    {
                        ModelState.AddModelError(string.Empty, firstError);
                    }
                }
            }
            return View(categoriaDTO);
        }

        public async Task<IActionResult> Edit(int id)
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
            if (ModelState.IsValid)
            {
                var response = await _categoriaService.AtualizarAsync(categoriaDTO);
                if (response.Success)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    var firstError = response.Errors?.FirstOrDefault();
                    if (firstError != null)
                    {
                        ModelState.AddModelError(string.Empty, firstError);
                    }
                }
            }
            return View(categoriaDTO);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var response = await _categoriaService.ObterPorIdAsync(id);
            if (response.Success && response.Data != null)
            {
                return View(response.Data);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _categoriaService.RemoverAsync(id);
            if (response.Success)
            {
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
