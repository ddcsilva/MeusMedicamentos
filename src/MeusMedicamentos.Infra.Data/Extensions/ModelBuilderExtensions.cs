using MeusMedicamentos.Domain.Entities;
using MeusMedicamentos.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MeusMedicamentos.Infra.Data.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void SeedData(this ModelBuilder modelBuilder)
        {
            PopularUsuariosERoles(modelBuilder);
            PopularCategorias(modelBuilder);
        }

        private static void PopularUsuariosERoles(ModelBuilder modelBuilder)
        {
            var adminRoleId = Guid.NewGuid();
            var adminUserId = Guid.NewGuid();
            var usuarioRoleId = Guid.NewGuid();

            var hasher = new PasswordHasher<Usuario>();

            // Adiciona o papel de administrador
            modelBuilder.Entity<IdentityRole<Guid>>().HasData(new IdentityRole<Guid>
            {
                Id = adminRoleId,
                Name = "Administrador",
                NormalizedName = "ADMINISTRADOR"
            });

            // Adiciona o papel de usuario
            modelBuilder.Entity<IdentityRole<Guid>>().HasData(new IdentityRole<Guid>
            {
                Id = usuarioRoleId,
                Name = "Usuario",
                NormalizedName = "USUARIO"
            });

            // Adiciona o usuário administrador
            var adminUser = new Usuario
            {
                Id = adminUserId,
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@meusmedicamentos.com",
                NormalizedEmail = "ADMIN@MEUSMEDICAMENTOS.COM",
                EmailConfirmed = true,
                Nome = "Administrador do Sistema"
            };

            // Hash da senha do usuário administrador
            adminUser.PasswordHash = hasher.HashPassword(adminUser, "Admin@123");

            modelBuilder.Entity<Usuario>().HasData(adminUser);

            // Associa o usuário administrador ao papel de administrador
            modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(new IdentityUserRole<Guid>
            {
                RoleId = adminRoleId,
                UserId = adminUserId
            });
        }

        private static void PopularCategorias(ModelBuilder modelBuilder)
        {
            var adminUserId = Guid.NewGuid();

            modelBuilder.Entity<Categoria>().HasData(new
            {
                Id = Guid.NewGuid(),
                Nome = "Analgésicos",
                Status = EStatus.Ativo,
                DataCriacao = DateTime.UtcNow,
                UsuarioCriacaoId = adminUserId
            });

            modelBuilder.Entity<Categoria>().HasData(new
            {
                Id = Guid.NewGuid(),
                Nome = "Antibióticos",
                Status = EStatus.Ativo,
                DataCriacao = DateTime.UtcNow,
                UsuarioCriacaoId = adminUserId
            });

            modelBuilder.Entity<Categoria>().HasData(new
            {
                Id = Guid.NewGuid(),
                Nome = "Anti-inflamatórios",
                Status = EStatus.Ativo,
                DataCriacao = DateTime.UtcNow,
                UsuarioCriacaoId = adminUserId
            });

            modelBuilder.Entity<Categoria>().HasData(new
            {
                Id = Guid.NewGuid(),
                Nome = "Antipiréticos",
                Status = EStatus.Inativo,
                DataCriacao = DateTime.UtcNow,
                UsuarioCriacaoId = adminUserId
            });

            modelBuilder.Entity<Categoria>().HasData(new
            {
                Id = Guid.NewGuid(),
                Nome = "Antissépticos",
                Status = EStatus.Inativo,
                DataCriacao = DateTime.UtcNow,
                UsuarioCriacaoId = adminUserId
            });

            modelBuilder.Entity<Categoria>().HasData(new
            {
                Id = Guid.NewGuid(),
                Nome = "Broncodilatadores",
                Status = EStatus.Ativo,
                DataCriacao = DateTime.UtcNow,
                UsuarioCriacaoId = adminUserId
            });
        }
    }
}
