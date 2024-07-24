using MeusMedicamentos.Infra.Data.Configurations;
using MeusMedicamentos.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MeusMedicamentos.Infra.Data.Context;

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