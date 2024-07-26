using MeusMedicamentos.Domain.Entities;
using MeusMedicamentos.Infra.Data.Configurations;
using MeusMedicamentos.Infra.Data.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MeusMedicamentos.Infra.Data.Context
{
    public class MeusMedicamentosContext : IdentityDbContext<Usuario, IdentityRole, string, IdentityUserClaim<string>, IdentityUserRole<string>, IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>
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

            // Configurar nomes de tabelas do Identity
            ConfigurarNomesTabelasIdentity(modelBuilder);

            // Popular dados iniciais
            modelBuilder.SeedData();
        }

        private static void ConfigurarNomesTabelasIdentity(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>(b =>
            {
                b.ToTable("TB_USUARIO");
            });

            modelBuilder.Entity<IdentityRole>(b =>
            {
                b.ToTable("TB_PAPEIS");
            });

            modelBuilder.Entity<IdentityUserRole<string>>(b =>
            {
                b.ToTable("TB_USUARIOS_PAPEIS");
            });

            modelBuilder.Entity<IdentityUserClaim<string>>(b =>
            {
                b.ToTable("TB_USUARIOS_CLAIMS");
            });

            modelBuilder.Entity<IdentityUserLogin<string>>(b =>
            {
                b.ToTable("TB_USUARIOS_LOGINS");
            });

            modelBuilder.Entity<IdentityRoleClaim<string>>(b =>
            {
                b.ToTable("TB_PAPEIS_CLAIMS");
            });

            modelBuilder.Entity<IdentityUserToken<string>>(b =>
            {
                b.ToTable("TB_USUARIOS_TOKENS");
            });
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
            }
        }
    }
}
