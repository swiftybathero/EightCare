using System;
using System.Threading.Tasks;
using AutoFixture;
using EightCare.Domain.Entities;
using EightCare.Infrastructure.Persistence;
using EightCare.Infrastructure.Persistence.Repositories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace EightCare.Infrastructure.UnitTests.Repositories
{
    public class CollectionRepositoryTests
    {
        private readonly CollectionRepository _collectionRepository;
        private readonly IFixture _fixture;

        public CollectionRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<CollectionContext>()
                          .UseInMemoryDatabase("CollectionsDb")
                          .Options;

            _collectionRepository = new CollectionRepository(new CollectionContext(options));
            _fixture = new Fixture();
        }

        [Fact]
        public async Task AddAsync_AddsCollection()
        {
            // Arrange
            var collection = _fixture.Create<Collection>();

            // Act
            await _collectionRepository.AddAsync(collection);

            // Assert
            await AssertCollectionExists(collection.Id);
        }

        [Fact]
        public async Task RemoveAsync_WithExistingCollection_RemovesCollection()
        {
            // Arrange
            var collection = _fixture.Create<Collection>();
            await AddCollection(collection);

            // Act
            await _collectionRepository.DeleteAsync(collection.Id);

            // Assert
            await AssertCollectionDoesNotExists(collection.Id);
        }

        private async Task AssertCollectionExists(Guid collectionId)
        {
            (await _collectionRepository.GetByIdAsync(collectionId)).Should().NotBeNull();
        }

        private async Task AssertCollectionDoesNotExists(Guid collectionId)
        {
            (await _collectionRepository.GetByIdAsync(collectionId)).Should().BeNull();
        }

        private async Task AddCollection(Collection collection)
        {
            await _collectionRepository.AddAsync(collection);
        }
    }
}
