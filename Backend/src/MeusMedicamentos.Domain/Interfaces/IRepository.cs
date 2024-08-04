using System.Linq.Expressions;

namespace MeusMedicamentos.Domain.Interfaces;

public interface IRepository<TEntity> : IDisposable where TEntity : class
{
    Task<IEnumerable<TEntity>> ObterTodosAsync(bool rastrearAlteracoes = false);
    Task<IEnumerable<TEntity>> ObterPorCondicaoAsync(Expression<Func<TEntity, bool>> expression, bool rastrearAlteracoes = false);
    Task<TEntity?> ObterPorIdAsync(Guid id, bool rastrearAlteracoes = false);
    Task AdicionarAsync(TEntity entity);
    Task AtualizarAsync(TEntity entity);
    Task RemoverAsync(TEntity entity);
}