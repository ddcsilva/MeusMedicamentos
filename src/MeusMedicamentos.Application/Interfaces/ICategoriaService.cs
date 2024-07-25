using MeusMedicamentos.Application.DTOs;
using MeusMedicamentos.Shared;

namespace MeusMedicamentos.Application.Interfaces
{
    public interface ICategoriaService
    {
        Task<ApiResponse<IEnumerable<CategoriaDTO>>> ObterTodosAsync();
        Task<ApiResponse<CategoriaDTO>> ObterPorIdAsync(int id);
        Task<ApiResponse<CategoriaDTO>> AdicionarAsync(CriarCategoriaDTO categoriaDTO);
        Task<ApiResponse<string>> AtualizarAsync(EditarCategoriaDTO categoriaDTO);
        Task<ApiResponse<string>> RemoverAsync(int id);
    }
}
