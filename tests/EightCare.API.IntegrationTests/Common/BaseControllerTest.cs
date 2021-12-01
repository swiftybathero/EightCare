using System.Net.Http;
using System.Threading.Tasks;
using AutoFixture;
using EightCare.Infrastructure.Common.Configuration;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Respawn;
using Xunit;

namespace EightCare.API.IntegrationTests.Common
{
    public abstract class BaseControllerTest : IClassFixture<WebApplicationFactory<Startup>>, IAsyncLifetime
    {
        protected readonly HttpClient Client;
        protected readonly IFixture Fixture;
        private string _checkpointConnectionString;

        private static readonly Checkpoint Checkpoint = new();

        protected BaseControllerTest(WebApplicationFactory<Startup> factory)
        {
            Client = factory.WithWebHostBuilder(builder =>
                            {
                                builder.ConfigureServices(services =>
                                {
                                    _checkpointConnectionString = services.BuildServiceProvider()
                                                                          .GetService<IOptions<DatabaseConfiguration>>()
                                                                          ?.Value
                                                                          .ConnectionString;
                                });
                            })
                            .CreateClient();

            Fixture = new Fixture();
        }

        public async Task InitializeAsync()
        {
            await Checkpoint.Reset(_checkpointConnectionString);
        }

        public Task DisposeAsync()
        {
            return Task.CompletedTask;
        }
    }
}
