using MeusMedicamentos.Domain.Interfaces;
using MeusMedicamentos.Infra.Data.Context;
using Microsoft.EntityFrameworkCore.Storage;

namespace MeusMedicamentos.Infra.Data.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly MeusMedicamentosContext _context;
    private IDbContextTransaction? _transaction;

    public UnitOfWork(MeusMedicamentosContext context)
    {
        _context = context;
    }

    public async Task<bool> SalvarAlteracoesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task IniciarTransacaoAsync()
    {
        _transaction = await _context.Database.BeginTransactionAsync();
    }

    public async Task SalvarTransacaoAsync()
    {
        if (_transaction != null)
        {
            await _transaction.CommitAsync();
        }
    }

    public async Task DescartarTransacaoAsync()
    {
        if (_transaction != null)
        {
            await _transaction.RollbackAsync();
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    public void Dispose()
    {
        _context.Dispose();
        _transaction?.Dispose();
    }
}