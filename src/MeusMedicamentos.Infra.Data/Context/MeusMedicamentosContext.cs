using MeusMedicamentos.Domain.Entities;
using MeusMedicamentos.Infra.Data.Configurations;
using MeusMedicamentos.Infra.Data.Extensions;
using Microsoft.EntityFrameworkCore;

namespace MeusMedicamentos.Infra.Data.Context
{
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

        public override int SaveChanges()
        {
            AtualizarDatasDeModificacao();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            AtualizarDatasDeModificacao();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void AtualizarDatasDeModificacao()
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is Entity && e.State == EntityState.Modified);

            foreach (var entry in entries)
            {
                var entity = (Entity)entry.Entity;
                entity.SetDataModificacao();
                // Aqui você pode definir o usuário de modificação se estiver disponível no contexto
                // entity.UsuarioModificacao = "Usuário atual";
            }
        }
    }
}
