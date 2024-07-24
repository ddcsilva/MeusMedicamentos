using Microsoft.EntityFrameworkCore;

namespace MeusMedicamentos.Infra.Data.Configurations;

public static class Convencoes
{
    public static void AplicarConvencoes(ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            // Configurar o nome da tabela para TB_NOME_DA_CLASSE
            modelBuilder.Entity(entityType.ClrType).ToTable($"TB_{entityType.ClrType.Name.ToUpper()}");

            // Configurar a chave primária para PK_NOME_DA_TABELA
            var chavePrimaria = entityType.FindPrimaryKey();
            if (chavePrimaria != null)
            {
                chavePrimaria.SetName($"PK_{entityType.GetTableName()}");
            }

            // Configurar as chaves estrangeiras para FK_NOME_DA_TABELA
            foreach (var foreignKey in entityType.GetForeignKeys())
            {
                foreignKey.SetConstraintName($"FK_{entityType.GetTableName()}_{foreignKey.PrincipalEntityType.GetTableName()}");
            }

            // Configurar os índices para IX_NOME_DA_TABELA
            foreach (var index in entityType.GetIndexes())
            {
                index.SetDatabaseName($"IX_{entityType.GetTableName()}_{string.Join("_", index.Properties.Select(p => p.Name))}");
            }
        }
    }
}