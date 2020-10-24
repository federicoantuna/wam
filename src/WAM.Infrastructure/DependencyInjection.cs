using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using WAM.Application.Common.Interfaces;
using WAM.Infrastructure.Persistence;
using WAM.Infrastructure.Services;

namespace WAM.Infrastructure
{
    [ExcludeFromCodeCoverage]
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            _ = services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite(configuration.GetConnectionString("DefaultConnection"),
                soa => soa.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)),
                ServiceLifetime.Transient);

            _ = services.AddTransient<IApplicationDbContext>(sp => sp.GetService<ApplicationDbContext>());

            _ = services.AddScoped<IDomainEventService, DomainEventService>();

            return services;
        }
    }
}