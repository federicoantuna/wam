using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics.CodeAnalysis;

namespace WAM.CommandLineInterface
{
    public class Program
    {
        private static readonly IServiceProvider _serviceProvider;
        
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