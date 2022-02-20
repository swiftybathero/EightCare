using EightCare.API.Common;
using EightCare.API.FunctionalTests.Common.Extensions;
using EightCare.Infrastructure.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EightCare.API.FunctionalTests.Common
{
    public class TestApplicationFactory : WebApplicationFactory<Startup>
    {
        public string DatabaseConnectionString => GetDatabaseConnectionString();

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment(Environments.FunctionalTest);

            builder.ConfigureTestDatabase();

            builder.ConfigureServices(services =>
            {
                var serviceProvider = services.BuildServiceProvider();

                using var scope = serviceProvider.CreateScope();
                using var context = scope.ServiceProvider.GetRequiredService<CollectionContext>();

                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            });
        }

        private string GetDatabaseConnectionString()
        {
            using var scope = Services.CreateScope();
            using var context = scope.ServiceProvider.GetRequiredService<CollectionContext>();

            return context.Database.GetConnectionString();
        }
    }
}
