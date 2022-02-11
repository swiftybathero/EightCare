using System.Net.Http;
using System.Threading.Tasks;
using AutoFixture;
using Respawn;
using Xunit;

namespace EightCare.API.IntegrationTests.Common
{
    public abstract class BaseControllerTest : IClassFixture<TestApplicationFactory>, IAsyncLifetime
    {
        private readonly string _checkpointConnectionString;

        protected readonly HttpClient Client;
        protected readonly IFixture Fixture;

        private static readonly Checkpoint Checkpoint = new();

        protected BaseControllerTest(TestApplicationFactory factory)
        {
            _checkpointConnectionString = factory.DatabaseConnectionString;

            Client = factory.CreateClient();
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
