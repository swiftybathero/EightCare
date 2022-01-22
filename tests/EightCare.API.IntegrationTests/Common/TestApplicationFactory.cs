using EightCare.Infrastructure.Common.Configuration;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace EightCare.API.IntegrationTests.Common
{
    public class TestApplicationFactory : WebApplicationFactory<Startup>
    {
        public string ConnectionString => Services
                                          .GetRequiredService<IOptions<DatabaseConfiguration>>()
                                          .Value
                                          .ConnectionString;
    }
}
