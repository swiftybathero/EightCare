using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.AutoNSubstitute;
using EightCare.Application.Collections.Queries.GetCollectionById;
using EightCare.Application.Common.Interfaces;
using EightCare.Domain.Entities;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace EightCare.Application.UnitTests.Collections.Queries.GetCollectionById
{
    public class GetCollectionByIdQueryTests
    {
        private readonly IFixture _fixture;
        private readonly ICollectionRepository _collectionRepository;
        private readonly GetCollectionByIdQueryHandler _handler;

        public GetCollectionByIdQueryTests()
        {
            _fixture = new Fixture();
            _fixture.Customize(new AutoNSubstituteCustomization());

            _collectionRepository = _fixture.Freeze<ICollectionRepository>();

            _handler = new GetCollectionByIdQueryHandler(_fixture.Create<ICollectionRepository>());
        }

        [Fact]
        public async Task Handle_ShouldReturnCollection()
        {
            // Arrange
            var collection = _fixture.Create<Collection>();
            _collectionRepository.GetByIdAsync(Arg.Is(collection.Id)).Returns(collection);

            var query = new GetCollectionByIdQuery(collection.Id);

            // Act
            var collectionDto = await _handler.Handle(query, CancellationToken.None);

            // Assert
            collectionDto.Should().BeEquivalentTo(collection, options =>
            {
                options.ComparingByMembers<Collection>();
                options.ExcludingMissingMembers();
                return options;
            });
        }
    }
}
