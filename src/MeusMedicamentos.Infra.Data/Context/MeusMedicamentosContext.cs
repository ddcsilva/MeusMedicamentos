using MeusMedicamentos.Infra.Data.Configurations;
using MeusMedicamentos.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using MeusMedicamentos.Infra.Data.Extensions;

namespace MeusMedicamentos.Infra.Data.Context;

public class MeusMedicamentosContext : DbContext
{
    public MeusMedicamentosContext(DbContextOptions<MeusMedicamentosContext> options) : base(options) { }

    public DbSet<Categoria> Categorias => Set<Categoria>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Aplicar convenções de nomes de objetos do banco de dados
        Convencoes.AplicarConvencoes(modelBuilder);

        // Aplicar configurações específicas de mapeamento
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MeusMedicamentosContext).Assembly);

        // Popular dados iniciais
        modelBuilder.SeedData();
    }
}