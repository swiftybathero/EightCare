using AutoFixture;
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
        public void AddAnimal_ShouldAddAnimal()
        {
            // Arrange
            var keeper = _fixture.Create<Keeper>();
            var animal = _fixture.Create<Animal>();

            // Act
            keeper.AddAnimal(animal);

            // Assert
            keeper.Animals.Should().Contain(animal);
        }
    }
}
