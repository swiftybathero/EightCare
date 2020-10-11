using AutoFixture;
using EightCare.Domain.Exceptions;
using EightCare.Domain.KeeperAggregate;
using EightCare.Domain.Properties;
using EightCare.UnitTests.Builders;
using FluentAssertions;
using System;
using System.Linq;
using Xunit;

namespace EightCare.UnitTests
{
    public class KeeperAggregateTests
    {
        private readonly IFixture _fixture;
        private readonly KeeperBuilder _keeperBuilder;

        public KeeperAggregateTests()
        {
            _fixture = new Fixture();
            _keeperBuilder = new KeeperBuilder();
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
            var keeper = _keeperBuilder.Create();
            var expectedAnimal = _fixture.Create<Animal>();

            // Act
            var createdAnimal = keeper.AddNewAnimal(expectedAnimal.ScientificName, expectedAnimal.CommonName);

            // Assert
            createdAnimal.Should().BeEquivalentTo(expectedAnimal, options => options.ComparingByMembers<Animal>());
            keeper.Animals.Should().Contain(createdAnimal);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void AddNewAnimal_NoScientificName_ShouldThrowDomainException(string scientificName)
        {
            // Arrange
            var keeper = _keeperBuilder.Create();

            // Act // Assert
            keeper.Invoking(x => x.AddNewAnimal(scientificName, _fixture.Create<string>()))
                  .Should()
                  .Throw<KeeperDomainException>()
                  .WithMessage(ExceptionMessages.ScientificNameCannotBeEmpty);
        }

        [Fact]
        public void FeedAnimal_AnimalExists_ShouldFeedAnimal()
        {
            // Arrange
            const int FeedAmount = 1;

            var existingAnimalId = _fixture.Create<Guid>();
            var feedingDate = _fixture.Create<DateTime>();
            var keeper = _keeperBuilder.BuildDefault().WithAnimals(existingAnimalId).Create();
            var animal = keeper.Animals.First(x => x.Id == existingAnimalId);

            // Act
            keeper.FeedAnimal(existingAnimalId, FeedAmount, feedingDate);

            // Assert
            animal.Feedings.Should().ContainSingle(x => x.Date == feedingDate && x.Amount == FeedAmount);
        }

        [Fact]
        public void FeedAnimal_NoParameters_ShouldFeedAnimalWithDefaultValues()
        {
            // Arrange
            const int ExpectedDefaultAmount = 1;

            var existingAnimalId = _fixture.Create<Guid>();
            var keeper = _keeperBuilder.BuildDefault().WithAnimals(existingAnimalId).Create();
            var animal = keeper.Animals.First(x => x.Id == existingAnimalId);

            // Act
            keeper.FeedAnimal(existingAnimalId);

            // Assert
            animal.Feedings.Should().ContainSingle(x => x.Date != default && x.Amount == ExpectedDefaultAmount);
        }

        [Fact]
        public void FeedAnimal_AnimalDoesNotExist_ShouldThrowException()
        {
            // Arrange
            var keeper = _keeperBuilder.BuildDefault().WithAnimals().Create();
            var notExistingAnimalId = _fixture.Create<Guid>();

            // Act // Assert
            keeper.Invoking(x => x.FeedAnimal(notExistingAnimalId))
                  .Should()
                  .Throw<KeeperDomainException>()
                  .WithMessage(string.Format(ExceptionMessages.AnimalNotFound, notExistingAnimalId));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void FeedAnimal_AmountLowerThanOne_ShouldThrowException(int amount)
        {
            // Arrange
            var existingAnimalId = _fixture.Create<Guid>();
            var keeper = _keeperBuilder.BuildDefault().WithAnimals(existingAnimalId).Create();

            // Act // Assert
            keeper.Invoking(x => x.FeedAnimal(existingAnimalId, amount))
                  .Should()
                  .Throw<KeeperDomainException>()
                  .WithMessage(ExceptionMessages.FeedAmountCannotBeLowerThanOne);
        }
    }
}
