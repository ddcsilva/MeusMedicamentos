using MeusMedicamentos.Application.DTOs;
using MeusMedicamentos.Application.Interfaces;
using MeusMedicamentos.Domain.Notifications;
using MeusMedicamentos.Shared;
using Microsoft.AspNetCore.Mvc;

namespace MeusMedicamentos.API.Controllers
{
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
            return CustomResponse(response.Data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterPorId(int id)
        {
            var response = await _categoriaService.ObterPorIdAsync(id);
            return CustomResponse(response.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Adicionar([FromBody] CriarCategoriaDTO categoriaDTO)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var response = await _categoriaService.AdicionarAsync(categoriaDTO);
            return CustomResponse(response.Data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(int id, [FromBody] EditarCategoriaDTO categoriaDTO)
        {
            if (id != categoriaDTO.Id)
            {
                NotificarErro("Erro ao atualizar a categoria: Id da requisição difere do Id do objeto");
                return CustomResponse(new ApiResponse<string>("Erro ao atualizar a categoria: Id da requisição difere do Id do objeto"));
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var response = await _categoriaService.AtualizarAsync(categoriaDTO);
            return CustomResponse(response.Data);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remover(int id)
        {
            var response = await _categoriaService.RemoverAsync(id);
            return CustomResponse(response.Data);
        }
    }
}
