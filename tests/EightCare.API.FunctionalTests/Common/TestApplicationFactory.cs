using EightCare.API.Common.Extensions;
using EightCare.Infrastructure.Common.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;

namespace EightCare.API.FunctionalTests.Common
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
#pragma warning disable CS0219 // Variable is assigned but its value is never used
            const string FunctionalTestsEnvironment = Environments.FunctionalTest;
#pragma warning restore CS0219 // Variable is assigned but its value is never used
#if RELEASE
            builder.UseEnvironment(FunctionalTestsEnvironment);
#endif
            base.ConfigureWebHost(builder);
        }
    }
}
