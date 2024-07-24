using MeusMedicamentos.Domain.Entities;
using MeusMedicamentos.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace MeusMedicamentos.Infra.Data.Extensions;

public static class ModelBuilderExtensions
{
    public static void SeedData(this ModelBuilder modelBuilder)
    {
        PopularCategorias(modelBuilder);
    }

    private static void PopularCategorias(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Categoria>().HasData(new
        {
            Id = 1,
            Nome = "Analgésicos",
            Status = Status.Ativo,
            DataCriacao = DateTime.UtcNow,
            UsuarioCriacao = "Sistema"
        });

        modelBuilder.Entity<Categoria>().HasData(new
        {
            Id = 2,
            Nome = "Antibióticos",
            Status = Status.Ativo,
            DataCriacao = DateTime.UtcNow,
            UsuarioCriacao = "Sistema"
        });

        modelBuilder.Entity<Categoria>().HasData(new
        {
            Id = 3,
            Nome = "Anti-inflamatórios",
            Status = Status.Ativo,
            DataCriacao = DateTime.UtcNow,
            UsuarioCriacao = "Sistema"
        });

        modelBuilder.Entity<Categoria>().HasData(new
        {
            Id = 4,
            Nome = "Antipiréticos",
            Status = Status.Inativo,
            DataCriacao = DateTime.UtcNow,
            UsuarioCriacao = "Sistema"
        });

        modelBuilder.Entity<Categoria>().HasData(new
        {
            Id = 5,
            Nome = "Antissépticos",
            Status = Status.Inativo,
            DataCriacao = DateTime.UtcNow,
            UsuarioCriacao = "Sistema"
        });

        modelBuilder.Entity<Categoria>().HasData(new
        {
            Id = 6,
            Nome = "Broncodilatadores",
            Status = Status.Ativo,
            DataCriacao = DateTime.UtcNow,
            UsuarioCriacao = "Sistema"
        });
    }
}