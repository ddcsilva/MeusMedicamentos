using Microsoft.EntityFrameworkCore;

namespace MeusMedicamentos.Infra.Data.Configurations
{
    public static class Convencoes
    {
        public static void AplicarConvencoes(ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                // Configurar o nome da tabela para TB_NOME_DA_CLASSE
                modelBuilder.Entity(entityType.ClrType).ToTable($"TB_{entityType.ClrType.Name.ToUpper()}", t =>
                {
                    // Configurar os checks para CK_NOME_DO_CHECK
                    var checkConstraints = entityType.GetCheckConstraints();
                    if (checkConstraints != null)
                    {
                        foreach (var constraint in checkConstraints)
                        {
                            var constraintName = constraint.Name?.ToUpper();
                            var constraintSql = constraint.Sql;
                            if (constraintName != null && constraintSql != null)
                            {
                                t.HasCheckConstraint($"CK_{constraintName}", constraintSql);
                            }
                        }
                    }
                });

                // Configurar a chave primária para PK_NOME_DA_PROPRIEDADE
                var chavePrimaria = entityType.FindPrimaryKey();
                if (chavePrimaria != null)
                {
                    foreach (var key in chavePrimaria.Properties)
                    {
                        key.SetColumnName($"PK_{key.Name.ToUpper()}");
                    }
                }

                // Configurar as chaves estrangeiras para FK_NOME_DA_PROPRIEDADE
                var foreignKeys = entityType.GetForeignKeys();
                if (foreignKeys != null)
                {
                    foreach (var chaveEstrangeira in foreignKeys)
                    {
                        foreach (var key in chaveEstrangeira.Properties)
                        {
                            key.SetColumnName($"FK_{key.Name.ToUpper()}");
                        }
                    }
                }

                // Configurar os índices para IX_NOME_DA_PROPRIEDADE
                var indexes = entityType.GetIndexes();
                if (indexes != null)
                {
                    foreach (var indice in indexes)
                    {
                        foreach (var property in indice.Properties)
                        {
                            property.SetColumnName($"IX_{property.Name.ToUpper()}");
                        }
                    }
                }
            }
        }
    }
}
