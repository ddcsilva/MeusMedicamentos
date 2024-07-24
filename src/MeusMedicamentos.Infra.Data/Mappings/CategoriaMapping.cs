using MeusMedicamentos.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MeusMedicamentos.Infra.Data.Mappings;

public class CategoriaMapping : IEntityTypeConfiguration<Categoria>
{
    public void Configure(EntityTypeBuilder<Categoria> builder)
    {
        // Chave primária
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .HasColumnName("CTG_ID")
            .HasColumnOrder(1)
            .HasColumnType("int")
            .IsRequired()
            .HasComment("Chave primária da categoria");

        // Propriedades específicas
        builder.Property(c => c.Nome)
            .HasColumnName("CTG_NOME")
            .HasColumnOrder(2)
            .HasMaxLength(100)
            .IsRequired()
            .HasColumnType("varchar(100)")
            .HasComment("Nome da Categoria");

        // Propriedades comuns
        builder.Property(c => c.Status)
            .HasColumnName("CTG_STATUS")
            .HasColumnOrder(3)
            .IsRequired()
            .HasColumnType("int")
            .HasComment("Status da Categoria");

        builder.Property(c => c.DataCriacao)
            .HasColumnName("CTG_DATA_CRIACAO")
            .HasColumnOrder(4)
            .IsRequired()
            .HasColumnType("datetime")
            .HasComment("Data da Criação da Categoria");

        builder.Property(c => c.DataModificacao)
            .HasColumnName("CTG_DATA_MODIFICACAO")
            .HasColumnOrder(5)
            .HasColumnType("datetime")
            .HasComment("Data da Última Modificação da Categoria");

        builder.Property(c => c.UsuarioCriacao)
            .HasColumnName("CTG_USUARIO_CRIACAO")
            .HasColumnOrder(6)
            .HasMaxLength(50)
            .HasColumnType("varchar(50)")
            .HasComment("Usuário Responsável pela Criação da Categoria");

        builder.Property(c => c.UsuarioModificacao)
            .HasColumnName("CTG_USUARIO_MODIFICACAO")
            .HasColumnOrder(7)
            .HasMaxLength(50)
            .HasColumnType("varchar(50)")
            .HasComment("Usuário Responsável pela Modificação da Categoria");

        // Índices
        builder.HasIndex(c => c.Nome)
            .HasDatabaseName("IX_CATEGORIA_NOME")
            .IsUnique();
    }
}
