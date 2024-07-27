using MeusMedicamentos.Domain.Entities;
using MeusMedicamentos.Infra.Data.Configurations;
using MeusMedicamentos.Infra.Data.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MeusMedicamentos.Infra.Data.Context
{
    public class MeusMedicamentosContext : IdentityDbContext<Usuario, IdentityRole<Guid>, Guid, IdentityUserClaim<Guid>, IdentityUserRole<Guid>, IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
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

            // Configurar relacionamento entre Categoria e Usuario
            ConfigurarRelacionamentos(modelBuilder);

            // Popular dados iniciais
            modelBuilder.SeedData();
        }

        private static void ConfigurarNomesTabelasIdentity(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>(b =>
            {
                b.ToTable("TB_USUARIO");
            });

            modelBuilder.Entity<IdentityRole<Guid>>(b =>
            {
                b.ToTable("TB_PAPEIS");
            });

            modelBuilder.Entity<IdentityUserRole<Guid>>(b =>
            {
                b.ToTable("TB_USUARIOS_PAPEIS");
            });

            modelBuilder.Entity<IdentityUserClaim<Guid>>(b =>
            {
                b.ToTable("TB_USUARIOS_CLAIMS");
            });

            modelBuilder.Entity<IdentityUserLogin<Guid>>(b =>
            {
                b.ToTable("TB_USUARIOS_LOGINS");
            });

            modelBuilder.Entity<IdentityRoleClaim<Guid>>(b =>
            {
                b.ToTable("TB_PAPEIS_CLAIMS");
            });

            modelBuilder.Entity<IdentityUserToken<Guid>>(b =>
            {
                b.ToTable("TB_USUARIOS_TOKENS");
            });
        }

        private static void ConfigurarRelacionamentos(ModelBuilder modelBuilder)
        {
            // Configurar relacionamento entre Categoria e UsuarioCriacao
            modelBuilder.Entity<Categoria>()
                .HasOne(c => c.UsuarioCriacao)
                .WithMany(u => u.CategoriasCriadas)
                .HasForeignKey(c => c.UsuarioCriacaoId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configurar relacionamento entre Categoria e UsuarioModificacao
            modelBuilder.Entity<Categoria>()
                .HasOne(c => c.UsuarioModificacao)
                .WithMany(u => u.CategoriasModificadas)
                .HasForeignKey(c => c.UsuarioModificacaoId)
                .OnDelete(DeleteBehavior.Restrict);
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
