using MeusMedicamentos.Application.DTOs;

namespace MeusMedicamentos.Application.Interfaces;

public interface ICategoriaService
{
    Task<IEnumerable<CategoriaDTO>> ObterTodosAsync();
    Task<CategoriaDTO?> ObterPorIdAsync(int id);
    Task AdicionarAsync(CriarCategoriaDTO categoriaDto);
    Task AtualizarAsync(EditarCategoriaDTO categoriaDto);
    Task RemoverAsync(int id);
}
