using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using AutoFixture;
using EightCare.API.Constants;
using EightCare.API.IntegrationTests.Common.Extensions;
using EightCare.Application.Collections.Commands.RegisterCollection;
using EightCare.Application.Collections.Queries.GetCollectionById;
using EightCare.Infrastructure.Common.Configuration;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Respawn;
using Xunit;

namespace EightCare.API.IntegrationTests.Controllers
{
    public class CollectionsControllerTests : IClassFixture<WebApplicationFactory<Startup>>, IAsyncLifetime
    {
        private readonly HttpClient _client;
        private readonly IFixture _fixture;
        private string _checkpointConnectionString;

        private static readonly Checkpoint Checkpoint = new();

        public CollectionsControllerTests(WebApplicationFactory<Startup> factory)
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
        public async Task RegisterCollection_ShouldCreateCollection()
        {
            // Arrange // Act
            var response = await CallCreateCollectionAsync();

            // Assert
            response.EnsureSuccessStatusCode();

            var createdId = await response.Content.GetIdAsync();
            createdId.Should().NotBeNullOrEmpty();
            response.Headers.Location.Should().NotBeNull();
            response.Headers.Location?.OriginalString.Should().Be($"{Routes.CollectionRoute}/{createdId}");
        }

        [Fact]
        public async Task GetCollectionById_ShouldReturnCreatedCollection()
        {
            // Arrange
            var createCollectionResult = await CallCreateCollectionAsync();
            var createdCollectionId = await createCollectionResult.Content.GetIdAsync();

            // Act
            var createdCollection = await _client.GetFromJsonAsync<CollectionDto>($"{Routes.CollectionRoute}/{createdCollectionId}");

            // Assert
            createdCollection.Should().NotBeNull();
            createdCollection?.Id.ToString().Should().Be(createdCollectionId);
        }

        private async Task<HttpResponseMessage> CallCreateCollectionAsync()
        {
            var registerCollectionCommand = _fixture.Create<RegisterCollectionCommand>();

            var response = await _client.PostAsJsonAsync(Routes.CollectionRoute, registerCollectionCommand);
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
