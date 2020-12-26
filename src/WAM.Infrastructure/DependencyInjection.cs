using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics.CodeAnalysis;
using WAM.Application.Common.Interfaces;
using WAM.Infrastructure.Persistence;
using WAM.Infrastructure.Services;

namespace WAM.Infrastructure
{
    [ExcludeFromCodeCoverage]
    public static class DependencyInjection
    {
        private const String StartupProjectAssemblyName = "WAM.CommandLineInterface";

        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            _ = services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite(configuration.GetConnectionString("DefaultConnection"),
                soa => soa.MigrationsAssembly(StartupProjectAssemblyName)));

            _ = services.AddTransient<IApplicationDbContext>(sp => sp.GetService<ApplicationDbContext>());

            _ = services.AddScoped<IDomainEventService, DomainEventService>();

            return services;
        }
    }
}