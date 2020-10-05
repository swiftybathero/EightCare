using AutoFixture;
using EightCare.Domain.Exceptions;
using EightCare.Domain.KeeperAggregate;
using FluentAssertions;
using Xunit;

namespace EightCare.UnitTests
{
    public class KeeperAggregateTests
    {
        private readonly IFixture _fixture;

        public KeeperAggregateTests()
        {
            _fixture = new Fixture();
        }

        [Fact]
        public void New_ShouldCreateKeeper()
        {
            // Arrange
            var name = _fixture.Create<string>();
            var email = _fixture.Create<string>();
            var age = _fixture.Create<int>();

            // Act
            var keeper = new Keeper(name, email, age);

            // Assert
            keeper.Should().NotBeNull();
            keeper.Name.Should().Be(name);
            keeper.Email.Should().Be(email);
            keeper.Age.Should().Be(age);
        }

        [Fact]
        public void AddNewAnimal_ShouldAddAnimal()
        {
            // Arrange
            var keeper = _fixture.Create<Keeper>();
            var expectedAnimal = _fixture.Create<Animal>();

            // Act
            keeper.AddNewAnimal(expectedAnimal.ScientificName, expectedAnimal.CommonName);

            // Assert
            keeper.Animals.Should().ContainEquivalentOf(expectedAnimal, config => config.ComparingByMembers<Animal>());
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void AddNewAnimal_NoScientificName_ShouldThrowDomainException(string scientificName)
        {
            // Arrange
            var keeper = _fixture.Create<Keeper>();

            // Act // Assert
            keeper.Invoking(x => x.AddNewAnimal(scientificName, _fixture.Create<string>()))
                  .Should()
                  .Throw<KeeperDomainException>();
        }
    }
}
