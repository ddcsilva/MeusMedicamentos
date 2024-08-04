using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using MeusMedicamentos.Domain.Entities;

namespace MeusMedicamentos.Infra.Data.Configurations;

public static class IdentityConfiguration
{
    public static void AplicarConfiguracoesIdentity(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Usuario>(b =>
        {
            b.ToTable("TB_USUARIO");
        });

        modelBuilder.Entity<IdentityRole<Guid>>(b =>
        {
            b.ToTable("TB_ROLES");
        });

        modelBuilder.Entity<IdentityUserRole<Guid>>(b =>
        {
            b.ToTable("TB_USUARIOS_ROLES");
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
            b.ToTable("TB_ROLES_CLAIMS");
        });

        modelBuilder.Entity<IdentityUserToken<Guid>>(b =>
        {
            b.ToTable("TB_USUARIOS_TOKENS");
        });
    }
}