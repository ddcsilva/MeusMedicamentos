using MeusMedicamentos.Domain.Entities;
using MeusMedicamentos.Infra.Data.Configurations;
using MeusMedicamentos.Infra.Data.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MeusMedicamentos.Infra.Data.Context
{
    public class MeusMedicamentosContext : IdentityDbContext<Usuario, IdentityRole<Guid>, Guid, IdentityUserClaim<Guid>, IdentityUserRole<Guid>, IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
    {
        public MeusMedicamentosContext(DbContextOptions<MeusMedicamentosContext> options) : base(options) { }

        public DbSet<Categoria> Categorias => Set<Categoria>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.AplicarConvencoes();
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MeusMedicamentosContext).Assembly);
            modelBuilder.AplicarConfiguracoesIdentity();
            modelBuilder.AplicarConfiguracoesRelacionamentos();
            modelBuilder.PopularDados();
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
                entity.DefinirDataModificacao();
            }
        }
    }
}
