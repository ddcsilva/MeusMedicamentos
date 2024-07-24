using MeusMedicamentos.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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

    }

    private static void ResolverDependenciasServices(this IServiceCollection services)
    {

    }

    private static IServiceCollection AdicionarContexto(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<MeusMedicamentosContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("MeusMedicamentosConnection"),
                b => b.MigrationsAssembly(typeof(MeusMedicamentosContext).Assembly.FullName)));

        return services;
    }

    private static void AdicionarAutoMapper(this IServiceCollection services)
    {

    }
}