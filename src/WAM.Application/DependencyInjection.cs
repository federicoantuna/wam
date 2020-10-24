using AutoMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using WAM.Application.Common.Behaviours;

namespace WAM.Application
{
    /// <summary>
    /// Provides the Application Dependency Injection.
    /// </summary>
    // ExcludeFromCodeCoverage: There is no value in testing this and it is extremely hard to test.
    [ExcludeFromCodeCoverage]
    public static class DependencyInjection
    {
        /// <summary>
        /// Adds the corresponding services for the application layer.
        /// </summary>
        /// <param name="services">The services collection.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            var executingAssembly = Assembly.GetExecutingAssembly();

            _ = services.AddAutoMapper(executingAssembly);
            _ = services.AddMediatR(executingAssembly);
            _ = services.AddMediatR(executingAssembly);

            _ = services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            _ = services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));

            return services;
        }
    }
}