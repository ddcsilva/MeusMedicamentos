using Microsoft.EntityFrameworkCore;

namespace MeusMedicamentos.Infra.Data.Configurations
{
    public static class Convencoes
    {
        public static void AplicarConvencoes(ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                modelBuilder.Entity(entityType.ClrType).ToTable($"TB_{entityType.ClrType.Name.ToUpper()}");

                var chavePrimaria = entityType.FindPrimaryKey();
                if (chavePrimaria != null)
                {
                    chavePrimaria.SetName($"PK_{entityType.GetTableName()}");
                }

                foreach (var foreignKey in entityType.GetForeignKeys())
                {
                    foreignKey.SetConstraintName($"FK_{entityType.GetTableName()}_{foreignKey.PrincipalEntityType.GetTableName()}");
                }

                foreach (var index in entityType.GetIndexes())
                {
                    index.SetDatabaseName($"IX_{entityType.GetTableName()}_{string.Join("_", index.Properties.Select(p => p.Name))}");
                }
            }
        }
    }
}
