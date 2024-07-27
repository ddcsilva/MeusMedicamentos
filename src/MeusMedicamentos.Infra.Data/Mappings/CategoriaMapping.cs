using MeusMedicamentos.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MeusMedicamentos.Infra.Data.Mappings
{
    public class CategoriaMapping : IEntityTypeConfiguration<Categoria>
    {
        public void Configure(EntityTypeBuilder<Categoria> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .HasColumnName("CTG_ID")
                .HasColumnOrder(1)
                .HasColumnType("char(36)") // Usando char(36) para armazenar Guid como string
                .IsRequired()
                .HasComment("Chave primária da categoria");

            builder.Property(c => c.Nome)
                .HasColumnName("CTG_NOME")
                .HasColumnOrder(2)
                .HasMaxLength(100)
                .IsRequired()
                .HasColumnType("varchar(100)")
                .HasComment("Nome da Categoria");

            builder.Property(c => c.Status)
                .HasColumnName("CTG_STATUS")
                .HasColumnOrder(3)
                .IsRequired()
                .HasColumnType("bit")
                .HasComment("Status da Categoria");

            builder.Property(c => c.DataCriacao)
                .HasColumnName("CTG_DATA_CRIACAO")
                .HasColumnOrder(4)
                .IsRequired()
                .HasColumnType("datetime")
                .HasComment("Data da Criação da Categoria");

            builder.Property(c => c.UsuarioCriacaoId)
                .HasColumnName("CTG_USUARIO_CRIACAO_ID")
                .HasColumnOrder(5)
                .HasColumnType("char(36)")
                .IsRequired()
                .HasComment("ID do Usuário Responsável pela Criação da Categoria");

            builder.Property(c => c.DataModificacao)
                .HasColumnName("CTG_DATA_MODIFICACAO")
                .HasColumnOrder(6)
                .HasColumnType("datetime")
                .HasComment("Data da Última Modificação da Categoria")
                .IsRequired(false);

            builder.Property(c => c.UsuarioModificacaoId)
                .HasColumnName("CTG_USUARIO_MODIFICACAO_ID")
                .HasColumnOrder(7)
                .HasColumnType("char(36)")
                .HasComment("ID do Usuário Responsável pela Modificação da Categoria")
                .IsRequired(false);

            builder.HasIndex(c => c.Nome)
                .HasDatabaseName("IX_CATEGORIA_NOME")
                .IsUnique();
        }
    }
}
