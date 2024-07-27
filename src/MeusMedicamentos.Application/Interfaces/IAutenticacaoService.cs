using Microsoft.AspNetCore.Identity;

namespace MeusMedicamentos.Application.Interfaces
{
    public interface IAutenticacaoService
    {
        Task<string?> AutenticarAsync(string usuario, string senha);
        Task<IdentityResult> CriarUsuarioAsync(string userName, string senha, string nome, string email);
    }
}
