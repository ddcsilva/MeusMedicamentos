using MeusMedicamentos.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MeusMedicamentos.Infra.IoC;

public static class DependencyInjection
{
    public static IServiceCollection ResolverDependencias(this IServiceCollection services, IConfiguration configuration)
    {
        AdicionarContexto(services, configuration);
        AdicionarAutoMapper(services);
        ResolverDependenciasRepository(services);
        ResolverDependenciasServices(services);

        return services;
    }

    private static void ResolverDependenciasRepository(this IServiceCollection services)
    {
        // Adicionar os repositórios específicos
    }

    private static void ResolverDependenciasServices(this IServiceCollection services)
    {
        // Adicionar os serviços específicos
    }

    private static IServiceCollection AdicionarContexto(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<MeusMedicamentosContext>(options =>
            options.UseMySql(configuration.GetConnectionString("MeusMedicamentosConnection"),
                new MySqlServerVersion(new Version(8, 0, 21)),
                b => b.MigrationsAssembly("MeusMedicamentos.Infra.Data")));

        return services;
    }

    private static void AdicionarAutoMapper(this IServiceCollection services)
    {
        // Configuração do AutoMapper, se necessário
    }
}
