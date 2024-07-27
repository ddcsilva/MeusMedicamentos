using MeusMedicamentos.Application.DTOs;
using MeusMedicamentos.Shared;

namespace MeusMedicamentos.Application.Interfaces
{
    public interface ICategoriaService
    {
        Task<ApiResponse<IEnumerable<CategoriaDTO>>> ObterTodosAsync();
        Task<ApiResponse<CategoriaDTO>> ObterPorIdAsync(Guid id);
        Task<ApiResponse<CategoriaDTO>> AdicionarAsync(CriarCategoriaDTO categoriaDTO);
        Task<ApiResponse<CategoriaDTO>> AtualizarAsync(EditarCategoriaDTO categoriaDTO);
        Task<ApiResponse<bool>> RemoverAsync(Guid id);
    }
}
