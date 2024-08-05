using MeusMedicamentos.Contracts;
using MeusMedicamentos.LoggerService;

namespace MeusMedicamentos.Api.Extensions;

public static class ServiceExtensions
{
    public static void ConfigurarCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
           options.AddPolicy("CorsPolicy", builder =>
           {
               builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
           });
        });
    }

    public static void ConfigurarIntegracaoIIS(this IServiceCollection services)
    {
        services.Configure<IISOptions>(options =>
        {

        });
    }
    
    public static void ConfigurarLoggerService(this IServiceCollection services)
    {
        services.AddSingleton<ILoggerManager, LoggerManager>();
    }
}