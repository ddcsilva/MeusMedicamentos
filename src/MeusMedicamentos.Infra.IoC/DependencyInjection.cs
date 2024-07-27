using FluentValidation;
using MeusMedicamentos.Application.Interfaces;
using MeusMedicamentos.Application.Mappings;
using MeusMedicamentos.Application.Services;
using MeusMedicamentos.Domain.Entities;
using MeusMedicamentos.Domain.Interfaces;
using MeusMedicamentos.Domain.Notifications;
using MeusMedicamentos.Domain.Validations;
using MeusMedicamentos.Infra.Data.Context;
using MeusMedicamentos.Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using System;

namespace MeusMedicamentos.Infra.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection ResolverDependencias(this IServiceCollection services, IConfiguration configuration)
        {
            AdicionarContexto(services, configuration);
            AdicionarIdentity(services);
            AdicionarRepositorios(services);
            AdicionarUnitOfWork(services);
            AdicionarServicos(services);
            AdicionarValidadores(services);
            AdicionarAutoMapper(services);

            return services;
        }

        private static IServiceCollection AdicionarContexto(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<MeusMedicamentosContext>(options =>
                options.UseMySql(configuration.GetConnectionString("MeusMedicamentosConnection"),
                    new MySqlServerVersion(new Version(8, 0, 21)),
                    b => b.MigrationsAssembly("MeusMedicamentos.Infra.Data")));

            return services;
        }

        private static void AdicionarIdentity(this IServiceCollection services)
        {
            services.AddIdentity<Usuario, IdentityRole<Guid>>()
                .AddEntityFrameworkStores<MeusMedicamentosContext>()
                .AddDefaultTokenProviders();
        }

        private static void AdicionarRepositorios(this IServiceCollection services)
        {
            services.AddScoped<ICategoriaRepository, CategoriaRepository>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        }

        private static void AdicionarUnitOfWork(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        private static void AdicionarServicos(this IServiceCollection services)
        {
            services.AddScoped<ICategoriaService, CategoriaService>();
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<INotificadorErros, NotificadorErros>();
        }

        private static void AdicionarValidadores(this IServiceCollection services)
        {
            services.AddTransient<IValidator<Categoria>, CategoriaValidator>();
        }

        private static void AdicionarAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AutoMapperProfile));
        }
    }
}
