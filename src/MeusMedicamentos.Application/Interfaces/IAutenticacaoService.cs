namespace MeusMedicamentos.Application.Interfaces
{
    public interface IAutenticacaoService
    {
        Task<string?> AutenticarAsync(string usuario, string senha);
    }
}
