namespace MeusMedicamentos.Domain.Interfaces;

public interface IUnitOfWork : IDisposable
{
    Task<bool> SalvarAlteracoesAsync();
    Task IniciarTransacaoAsync();
    Task SalvarTransacaoAsync();
    Task DescartarTransacaoAsync();
}