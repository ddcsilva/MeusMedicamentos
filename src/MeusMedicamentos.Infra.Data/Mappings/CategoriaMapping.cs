using MeusMedicamentos.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MeusMedicamentos.Infra.Data.Mappings;

public class CategoriaMapping : IEntityTypeConfiguration<Categoria>
{
    public void Configure(EntityTypeBuilder<Categoria> builder)
    {
        builder.Property(c => c.Id)
            .HasColumnName("CTG_ID")
            .HasColumnOrder(1)
            .HasColumnType("int")
            .IsRequired()
            .HasComment("Chave primária da categoria");

        builder.Property(c => c.Nome)
            .HasColumnName("CTG_NOME")
            .HasColumnOrder(2)
            .HasMaxLength(100)
            .IsRequired()
            .HasColumnType("varchar(100)")
            .HasComment("Nome da categoria");

        builder.Property(c => c.DataCriacao)
            .HasColumnName("CTG_DATA_CRIACAO")
            .HasColumnOrder(3)
            .IsRequired()
            .HasColumnType("datetime")
            .HasComment("Data de criação da categoria");

        builder.HasIndex(c => c.Nome)
            .IsUnique();
    }
}