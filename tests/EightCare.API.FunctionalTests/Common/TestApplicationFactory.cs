using System;
using EightCare.API.Common;
using EightCare.Infrastructure.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EightCare.API.FunctionalTests.Common
{
    public class TestApplicationFactory : WebApplicationFactory<Startup>
    {
        public string DatabaseConnectionString => GetDatabaseConnectionString();

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment(Environments.FunctionalTest);

            builder.ConfigureAppConfiguration(configuration =>
                configuration.AddJsonFile("appsettings.Development.json")
            );

            if (ShouldRunAgainstProductionDatabase())
            {
                builder.ConfigureAppConfiguration(configuration =>
                {
                    configuration.AddJsonFile("appsettings.json");
                });
            }

            builder.ConfigureServices(services =>
            {
                var serviceProvider = services.BuildServiceProvider();

                using var scope = serviceProvider.CreateScope();
                using var context = scope.ServiceProvider.GetRequiredService<CollectionContext>();

                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            });
        }

        private static bool ShouldRunAgainstProductionDatabase()
        {
            return bool.TryParse(Environment.GetEnvironmentVariable("RunAgainstProductionDatabase"),
                out var testProductionDatabase) && testProductionDatabase;
        }

        private string GetDatabaseConnectionString()
        {
            using var scope = Services.CreateScope();
            using var context = scope.ServiceProvider.GetRequiredService<CollectionContext>();

            return context.Database.GetConnectionString();
        }
    }
}
