using AutoFixture;
using EightCare.Domain.KeeperAggregate;
using FluentAssertions;
using Xunit;

namespace EightCare.UnitTests
{
    public class AnimalTests
    {
        private readonly IFixture _fixture;

        public AnimalTests()
        {
            _fixture = new Fixture();
        }

        [Fact]
        public void New_ShouldCreateAnimal()
        {
            // Arrange
            var scientificName = _fixture.Create<string>();
            var commonName = _fixture.Create<string>();

            // Act
            var animal = new Animal(scientificName, commonName);

            // Assert
            animal.Should().NotBeNull();
            animal.ScientificName.Should().Be(scientificName);
            animal.CommonName.Should().Be(commonName);
        }
    }
}
