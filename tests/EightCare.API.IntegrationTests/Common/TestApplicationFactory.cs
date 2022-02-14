using EightCare.API.Common.Extensions;
using EightCare.Infrastructure.Common.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;

namespace EightCare.API.IntegrationTests.Common
{
    public class TestApplicationFactory : WebApplicationFactory<Startup>
    {
        private readonly IConfigurationRoot _configuration;

        public TestApplicationFactory()
        {
            _configuration = new ConfigurationBuilder()
                                .AddJsonFile("appsettings.json")
                                .AddJsonFile("appsettings.Development.json")
                                .AddEnvironmentVariables()
                                .Build();
        }

        public string DatabaseConnectionString => _configuration.GetSection(DatabaseConfiguration.Key)
                                                                .Get<DatabaseConfiguration>().ConnectionString;

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
#if RELEASE
            builder.UseEnvironment(Environments.FunctionalTest);
#endif
            base.ConfigureWebHost(builder);
        }
    }
}
