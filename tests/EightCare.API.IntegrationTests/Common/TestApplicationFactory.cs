using EightCare.Infrastructure.Common.Configuration;
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
    }
}
