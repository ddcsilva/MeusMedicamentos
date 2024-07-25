using MeusMedicamentos.Domain.Entities;
using MeusMedicamentos.Domain.Interfaces;
using MeusMedicamentos.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MeusMedicamentos.Infra.Data.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
{
    protected readonly MeusMedicamentosContext _context;
    protected readonly DbSet<TEntity> _dbSet;

    public Repository(MeusMedicamentosContext context)
    {
        _context = context;
        _dbSet = _context.Set<TEntity>();
    }

    public async Task<IEnumerable<TEntity>> ObterTodosAsync(bool rastrearAlteracoes = false)
    {
        return !rastrearAlteracoes
            ? await _dbSet.AsNoTracking().ToListAsync()
            : await _dbSet.ToListAsync();
    }

    public async Task<IEnumerable<TEntity>> ObterPorCondicaoAsync(Expression<Func<TEntity, bool>> expression, bool rastrearAlteracoes = false)
    {
        return !rastrearAlteracoes
            ? await _dbSet.AsNoTracking().Where(expression).ToListAsync()
            : await _dbSet.Where(expression).ToListAsync();
    }

    public async Task<TEntity?> ObterPorIdAsync(int id, bool rastrearAlteracoes = false)
    {
        return !rastrearAlteracoes
            ? await _dbSet.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id)
            : await _dbSet.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task AdicionarAsync(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public Task AtualizarAsync(TEntity entity)
    {
        _dbSet.Update(entity);
        return Task.CompletedTask;
    }

    public Task RemoverAsync(TEntity entity)
    {
        _dbSet.Remove(entity);
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}