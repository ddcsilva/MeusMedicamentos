using MeusMedicamentos.Application.DTOs;
using MeusMedicamentos.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MeusMedicamentos.Web.Controllers
{
    public class CategoriasController : Controller
    {
        private readonly ICategoriaService _categoriaService;

        public CategoriasController(ICategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        // Listar categorias
        public async Task<IActionResult> Index()
        {
            var response = await _categoriaService.ObterTodosAsync();
            if (response.Success)
            {
                return View(response.Data);
            }
            else
            {
                // Tratar erros conforme necessário
                return View(new List<CategoriaDTO>());
            }
        }

        // Exibir formulário de criação
        public IActionResult Create()
        {
            return View();
        }

        // Processar formulário de criação
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
                    // Tratar erros conforme necessário
                    ModelState.AddModelError(string.Empty, response.Errors.FirstOrDefault());
                }
            }
            return View(categoriaDTO);
        }

        // Exibir formulário de edição
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _categoriaService.ObterPorIdAsync(id);
            if (response.Success)
            {
                var categoriaDTO = new EditarCategoriaDTO
                {
                    Id = response.Data.Id,
                    Nome = response.Data.Nome,
                    Status = response.Data.Status
                };
                return View(categoriaDTO);
            }
            return RedirectToAction(nameof(Index));
        }

        // Processar formulário de edição
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
                    // Tratar erros conforme necessário
                    ModelState.AddModelError(string.Empty, response.Errors.FirstOrDefault());
                }
            }
            return View(categoriaDTO);
        }

        // Exibir formulário de exclusão
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _categoriaService.ObterPorIdAsync(id);
            if (response.Success)
            {
                return View(response.Data);
            }
            return RedirectToAction(nameof(Index));
        }

        // Processar formulário de exclusão
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _categoriaService.RemoverAsync(id);
            if (response.Success)
            {
                return RedirectToAction(nameof(Index));
            }
            // Tratar erros conforme necessário
            return RedirectToAction(nameof(Index));
        }
    }
}
