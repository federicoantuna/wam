using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics.CodeAnalysis;

namespace WAM.CommandLineInterface
{
    public class Program
    {
        private static readonly IServiceProvider _serviceProvider;

        // ExcludeFromCodeCoverage: There is no value in testing this.
        [ExcludeFromCodeCoverage]
        static Program()
        {
            _serviceProvider = Startup.ConfigureServices().BuildServiceProvider();
        }

        public static void Main(String[] args)
        {
        }
    }
}