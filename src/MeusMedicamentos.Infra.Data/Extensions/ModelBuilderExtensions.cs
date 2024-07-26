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
            var adminRoleId = "1";
            var adminUserId = "1";

            var hasher = new PasswordHasher<Usuario>();

            // Adiciona o papel de administrador
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = adminRoleId,
                Name = "Administrador",
                NormalizedName = "ADMINISTRADOR"
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
                Nome = "Administrador do Sistema",
                PasswordHash = hasher.HashPassword(null, "Admin@123")
            };

            modelBuilder.Entity<Usuario>().HasData(adminUser);

            // Associa o usuário administrador ao papel de administrador
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = adminRoleId,
                UserId = adminUserId
            });
        }

        private static void PopularCategorias(ModelBuilder modelBuilder)
        {
            var adminUserId = "1";

            modelBuilder.Entity<Categoria>().HasData(new
            {
                Id = 1,
                Nome = "Analgésicos",
                Status = EStatus.Ativo,
                DataCriacao = DateTime.UtcNow,
                UsuarioCriacaoId = adminUserId
            });

            modelBuilder.Entity<Categoria>().HasData(new
            {
                Id = 2,
                Nome = "Antibióticos",
                Status = EStatus.Ativo,
                DataCriacao = DateTime.UtcNow,
                UsuarioCriacaoId = adminUserId
            });

            modelBuilder.Entity<Categoria>().HasData(new
            {
                Id = 3,
                Nome = "Anti-inflamatórios",
                Status = EStatus.Ativo,
                DataCriacao = DateTime.UtcNow,
                UsuarioCriacaoId = adminUserId
            });

            modelBuilder.Entity<Categoria>().HasData(new
            {
                Id = 4,
                Nome = "Antipiréticos",
                Status = EStatus.Inativo,
                DataCriacao = DateTime.UtcNow,
                UsuarioCriacaoId = adminUserId
            });

            modelBuilder.Entity<Categoria>().HasData(new
            {
                Id = 5,
                Nome = "Antissépticos",
                Status = EStatus.Inativo,
                DataCriacao = DateTime.UtcNow,
                UsuarioCriacaoId = adminUserId
            });

            modelBuilder.Entity<Categoria>().HasData(new
            {
                Id = 6,
                Nome = "Broncodilatadores",
                Status = EStatus.Ativo,
                DataCriacao = DateTime.UtcNow,
                UsuarioCriacaoId = adminUserId
            });
        }
    }
}
