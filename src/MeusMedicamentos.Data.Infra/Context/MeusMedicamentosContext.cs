using MeusMedicamentos.Data.Infra.Configurations;
using MeusMedicamentos.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MeusMedicamentos.Data.Infra.Context;

public class MeusMedicamentosContext : DbContext
{
    public MeusMedicamentosContext(DbContextOptions<MeusMedicamentosContext> options) : base(options) { }

    public DbSet<Categoria> Categorias => Set<Categoria>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MeusMedicamentosContext).Assembly);

        Convencoes.AplicarConvencoes(modelBuilder);
    }
}