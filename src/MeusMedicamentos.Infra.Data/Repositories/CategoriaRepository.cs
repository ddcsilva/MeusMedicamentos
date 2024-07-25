using MeusMedicamentos.Domain.Entities;
using MeusMedicamentos.Domain.Interfaces;
using MeusMedicamentos.Infra.Data.Context;

namespace MeusMedicamentos.Infra.Data.Repositories
{
    public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
    {
        public CategoriaRepository(MeusMedicamentosContext context) : base(context)
        {
        }
    }
}
