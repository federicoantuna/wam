using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using WAM.Infrastructure.Persistence;

namespace WAM.CommandLineInterface
{
    // ExcludeFromCodeCoverage: There is no value in testing this and it is extremely hard to test.
    [ExcludeFromCodeCoverage]
    public static class Startup
    {
        private const String _connectionStringName = "Default";
        private const String _configFile = "config.json";

        public static IServiceCollection ConfigureServices()
        {
            var services = new ServiceCollection();

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(_configFile, optional: false, reloadOnChange: false);
            var configuration = (IConfiguration)builder.Build();

            _ = services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite(configuration.GetConnectionString(_connectionStringName)));

            _ = services.AddSingleton(configuration);

            return services;
        }
    }
}