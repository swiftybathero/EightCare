using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using AutoFixture;
using EightCare.API.Constants;
using EightCare.API.IntegrationTests.Common;
using EightCare.API.IntegrationTests.Common.Extensions;
using EightCare.Application.Collections.Commands.RegisterCollection;
using EightCare.Application.Collections.Queries.GetCollectionById;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Xunit;

namespace EightCare.API.IntegrationTests.Controllers
{
    public class CollectionsControllerTests : BaseControllerTest
    {
        public CollectionsControllerTests(TestApplicationFactory factory) : base(factory) { }

        [Fact]
        public async Task RegisterCollection_ShouldCreateCollection()
        {
            // Arrange // Act
            var response = await CallCreateCollectionAsync();

            // Assert
            var createdCollectionId = await response.Content.GetIdAsync();
            createdCollectionId.Should().NotBeNullOrEmpty();
            response.Headers.Location.Should().NotBeNull();
            response.Headers.Location?.OriginalString.Should().Be($"{Routes.CollectionRoute}/{createdCollectionId}");
        }

        [Fact]
        public async Task GetCollectionById_ShouldReturnCreatedCollection()
        {
            // Arrange
            var response = await CallCreateCollectionAsync();
            var createdCollectionId = await response.Content.GetIdAsync();

            // Act
            var createdCollection = await CallGetCollectionAsync(createdCollectionId);

            // Assert
            createdCollection.Should().NotBeNull();
            createdCollection?.Id.ToString().Should().Be(createdCollectionId);
        }

        [Fact]
        public async Task DeleteCollection_CollectionExists_ShouldDeleteCollection()
        {
            // Arrange
            var createCollectionResponse = await CallCreateCollectionAsync();
            var collectionId = await createCollectionResponse.Content.GetIdAsync();

            var createdCollection = await CallGetCollectionAsync(collectionId);
            createdCollection.Should().NotBeNull();

            // Act
            var deleteCollectionResponse = await Client.DeleteAsync($"{Routes.CollectionRoute}/{collectionId}");
            deleteCollectionResponse.EnsureSuccessStatusCode();

            var getCollectionResponse = await Client.GetAsync($"{Routes.CollectionRoute}/{collectionId}");

            // Assert
            getCollectionResponse.StatusCode.Should().Be(StatusCodes.Status404NotFound);
        }

        private async Task<HttpResponseMessage> CallCreateCollectionAsync()
        {
            var registerCollectionCommand = Fixture.Create<RegisterCollectionCommand>();

            var response = await Client.PostAsJsonAsync(Routes.CollectionRoute, registerCollectionCommand);
            response.EnsureSuccessStatusCode();

            return response;
        }

        private async Task<CollectionDto> CallGetCollectionAsync(string collectionId)
        {
            return await Client.GetFromJsonAsync<CollectionDto>($"{Routes.CollectionRoute}/{collectionId}");
        }
    }
}
