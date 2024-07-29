using Microsoft.AspNetCore.Identity;
using MeusMedicamentos.Application.DTOs.Usuario;
using MeusMedicamentos.Shared;

namespace MeusMedicamentos.Application.Interfaces
{
    public interface IUsuarioService
    {
        Task<ApiResponse<IEnumerable<UsuarioDTO>>> ObterTodosUsuariosAsync();
        Task<ApiResponse<UsuarioDTO>> ObterPorIdAsync(Guid id);
        Task<ApiResponse<UsuarioDTO>> CriarUsuarioAsync(string userName, string nome, string email);
        Task<ApiResponse<UsuarioDTO>> AtualizarUsuarioAsync(Guid id, string userName, string nome, string email);
        Task<ApiResponse<bool>> RemoverUsuarioAsync(Guid id);
        Task<string?> AutenticarAsync(string userName, string senha);
    }
}
