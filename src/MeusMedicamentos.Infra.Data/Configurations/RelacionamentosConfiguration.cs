using MeusMedicamentos.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MeusMedicamentos.Infra.Data.Configurations
{
    public static class RelacionamentosConfiguration
    {
        public static void AplicarConfiguracoesRelacionamentos(this ModelBuilder modelBuilder)
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
    }
}
