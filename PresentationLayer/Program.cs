#nullable enable
using System;
using BusinessLogicLayer.DataInitialize;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.EF;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace PresentationLayer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IWebHost? host = BuildWebHost(args);

            using (IServiceScope? scope = host.Services.CreateScope())
            {
                IServiceProvider? services = scope.ServiceProvider;
                ReportsDbContext? context = services.GetService<ReportsDbContext>();
                context?.Database.EnsureDeleted();
                IPasswordHasher? passwordHasher = services.GetService<IPasswordHasher>();
                DatabaseSeed.Seed(context, passwordHasher);
            }

            host.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}