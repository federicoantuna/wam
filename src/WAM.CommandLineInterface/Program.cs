using Microsoft.Extensions.DependencyInjection;
using System;

namespace WAM.CommandLineInterface
{
    public class Program
    {
        private static readonly IServiceProvider _serviceProvider;
        
        static Program()
        {
            _serviceProvider = Startup.ConfigureServices().BuildServiceProvider();
        }

        public static void Main(String[] args)
        {
        }
    }
}