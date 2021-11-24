using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using AutoFixture;
using EightCare.API.Constants;
using EightCare.API.IntegrationTests.Common.Extensions;
using EightCare.Application.Keepers.Commands.RegisterKeeper;
using EightCare.Application.Keepers.Queries.GetKeeperById;
using EightCare.Infrastructure.Common.Configuration;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Respawn;
using Xunit;

namespace EightCare.API.IntegrationTests.Controllers
{
    public class KeepersControllerTests : IClassFixture<WebApplicationFactory<Startup>>, IAsyncLifetime
    {
        private readonly HttpClient _client;
        private readonly IFixture _fixture;
        private string _checkpointConnectionString;

        private static readonly Checkpoint Checkpoint = new();

        public KeepersControllerTests(WebApplicationFactory<Startup> factory)
        {
            _client = factory.WithWebHostBuilder(builder =>
             {
                 builder.ConfigureServices(services =>
                 {
                     _checkpointConnectionString = services.BuildServiceProvider()
                                                           .GetService<IOptions<DatabaseConfiguration>>()?.Value
                                                           .ConnectionString;
                 });
             })
             .CreateClient();

            _fixture = new Fixture();
        }

        [Fact]
        public async Task RegisterKeeper_ShouldCreateKeeper()
        {
            // Arrange // Act
            var response = await CallCreateKeeperAsync();

            // Assert
            response.EnsureSuccessStatusCode();

            var createdId = await response.Content.GetIdAsync();
            createdId.Should().NotBeNullOrEmpty();
            response.Headers.Location.Should().NotBeNull();
            response.Headers.Location?.OriginalString.Should().Be($"{Routes.KeeperRoute}/{createdId}");
        }

        [Fact]
        public async Task GetKeeperById_ShouldReturnCreatedKeeper()
        {
            // Arrange
            var createKeeperResult = await CallCreateKeeperAsync();
            var createdKeeperId = await createKeeperResult.Content.GetIdAsync();

            // Act
            var createdKeeper = await _client.GetFromJsonAsync<KeeperDto>($"{Routes.KeeperRoute}/{createdKeeperId}");

            // Assert
            createdKeeper.Should().NotBeNull();
            createdKeeper?.Id.ToString().Should().Be(createdKeeperId);
        }

        private async Task<HttpResponseMessage> CallCreateKeeperAsync()
        {
            var registerKeeperCommand = _fixture.Create<RegisterKeeperCommand>();

            var response = await _client.PostAsJsonAsync(Routes.KeeperRoute, registerKeeperCommand);
            response.EnsureSuccessStatusCode();

            return response;
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
