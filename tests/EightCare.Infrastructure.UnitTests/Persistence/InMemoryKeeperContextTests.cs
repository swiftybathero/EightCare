using System.Linq;
using AutoFixture;
using EightCare.Domain.Entities;
using EightCare.Infrastructure.Persistence;
using FluentAssertions;
using Xunit;

namespace EightCare.Infrastructure.UnitTests.Persistence
{
    public class InMemoryKeeperContextTests
    {
        private readonly IFixture _fixture;
        private readonly InMemoryKeeperContext _keeperContext;

        public InMemoryKeeperContextTests()
        {
            _fixture = new Fixture();
            _keeperContext = new InMemoryKeeperContext();
        }

        [Fact]
        public void SaveChanges_ShouldSetIdForExistingKeepers()
        {
            // Arrange
            var keepers = _fixture.CreateMany<Keeper>();

            _keeperContext.Keepers.AddRange(keepers);

            // Act
            _keeperContext.SaveChanges();

            // Assert
            _keeperContext.Keepers.Select(x => x.Id).Should().OnlyHaveUniqueItems();
        }
    }
}
