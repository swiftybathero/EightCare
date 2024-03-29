using System;
using System.Linq;
using AutoFixture;
using EightCare.Domain.Entities;
using EightCare.Domain.Enums;
using EightCare.Domain.Exceptions;
using EightCare.Domain.Properties;
using FluentAssertions;
using Xunit;
using static EightCare.Domain.UnitTests.Common.Builders.CollectionBuilder;

namespace EightCare.Domain.UnitTests.Domain
{
    public class CollectionAggregateTests
    {
        private readonly IFixture _fixture;

        public CollectionAggregateTests()
        {
            _fixture = new Fixture();
        }

        [Fact]
        public void New_WithCorrectData_CreatesCollection()
        {
            // Arrange
            var userId = _fixture.Create<Guid>();
            var name = _fixture.Create<string>();

            // Act
            var collection = new Collection(userId, name);

            // Assert
            collection.Should().NotBeNull();
            collection.UserId.Should().Be(userId);
            collection.Name.Should().Be(name);
        }

        [Fact]
        public void AddNewAnimal_WithCorrectData_AddsAnimal()
        {
            // Arrange
            var collection = GivenCollection().Build();
            var expectedAnimal = _fixture.Create<Animal>();

            // Act
            var createdAnimal = collection.AddNewAnimal
            (
                expectedAnimal.Species.ScientificName,
                expectedAnimal.Species.CommonName,
                expectedAnimal.Name,
                expectedAnimal.Received,
                expectedAnimal.LifeStage,
                expectedAnimal.Sex
            );

            // Assert
            createdAnimal.Should().BeEquivalentTo(expectedAnimal, options => options.ComparingByMembers<Animal>());
            collection.Animals.Should().Contain(createdAnimal);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void AddNewAnimal_NoScientificName_ThrowsDomainException(string scientificName)
        {
            // Arrange
            var collection = GivenCollection().Build();

            // Act // Assert
            collection.Invoking(x => x.AddNewAnimal
                      (
                          scientificName,
                          _fixture.Create<string>(),
                          _fixture.Create<string>(),
                          _fixture.Create<DateTimeOffset>(),
                          _fixture.Create<LifeStage>(),
                          _fixture.Create<Sex>())
                      )
                      .Should()
                      .Throw<CollectionDomainException>()
                      .WithMessage(ExceptionMessages.ScientificNameCannotBeEmpty);
        }

        [Fact]
        public void FeedAnimal_AnimalExists_FeedsAnimal()
        {
            // Arrange
            const int FeedAmount = 1;

            var existingAnimalId = _fixture.Create<Guid>();
            var feedingDate = _fixture.Create<DateTimeOffset>();
            var collection = GivenCollection().WithAnimals(animals => animals.WithIds(existingAnimalId)).Build();
            var animal = collection.Animals.First(x => x.Id == existingAnimalId);

            // Act
            collection.FeedAnimal(existingAnimalId, FeedAmount, feedingDate);

            // Assert
            animal.Feedings.Should().ContainSingle(x => x.Date == feedingDate && x.Amount == FeedAmount);
        }

        [Fact]
        public void FeedAnimal_NoParameters_FeedsAnimalWithDefaultValues()
        {
            // Arrange
            const int ExpectedDefaultAmount = 1;

            var existingAnimalId = _fixture.Create<Guid>();
            var collection = GivenCollection().WithAnimals(animals => animals.WithIds(existingAnimalId)).Build();
            var animal = collection.Animals.First(x => x.Id == existingAnimalId);

            // Act
            collection.FeedAnimal(existingAnimalId);

            // Assert
            animal.Feedings.Should().ContainSingle(x => x.Date != default && x.Amount == ExpectedDefaultAmount);
        }

        [Fact]
        public void FeedAnimal_AnimalDoesNotExist_ThrowsDomainException()
        {
            // Arrange
            var collection = GivenCollection().WithAnimals().Build();
            var notExistingAnimalId = _fixture.Create<Guid>();

            // Act // Assert
            collection.Invoking(x => x.FeedAnimal(notExistingAnimalId))
                  .Should()
                  .Throw<CollectionDomainException>()
                  .WithMessage(string.Format(ExceptionMessages.AnimalNotFound, notExistingAnimalId));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void FeedAnimal_AmountEqualOrLowerThanOne_ThrowsDomainException(int amount)
        {
            // Arrange
            var existingAnimalId = _fixture.Create<Guid>();
            var collection = GivenCollection().WithAnimals(animals => animals.WithIds(existingAnimalId)).Build();

            // Act // Assert
            collection.Invoking(x => x.FeedAnimal(existingAnimalId, amount))
                  .Should()
                  .Throw<CollectionDomainException>()
                  .WithMessage(ExceptionMessages.FeedAmountCannotBeLowerThanOne);
        }

        [Fact]
        public void ReportMolt_WithCorrectAnimal_ReportsMolt()
        {
            // Arrange
            var existingAnimalId = _fixture.Create<Guid>();
            var collection = GivenCollection().WithAnimals(animals => animals.WithIds(existingAnimalId)).Build();

            var moltingDate = _fixture.Create<DateTimeOffset>();
            var moltingAnimal = collection.Animals.First(x => x.Id == existingAnimalId);

            // Act
            collection.ReportMolt(existingAnimalId, moltingDate);

            // Assert
            moltingAnimal.Molts.Should().Contain(x => x.Date == moltingDate);
        }

        [Fact]
        public void ReportMolt_NoDate_ReportsDefaultDate()
        {
            // Arrange
            var existingAnimalId = _fixture.Create<Guid>();
            var collection = GivenCollection().WithAnimals(animals => animals.WithIds(existingAnimalId)).Build();

            var moltingAnimal = collection.Animals.First(x => x.Id == existingAnimalId);

            // Act
            collection.ReportMolt(existingAnimalId);

            // Assert
            moltingAnimal.Molts.Should().NotContain(x => x.Date == default);
        }

        [Fact]
        public void ReportMolt_AnimalDoesNotExist_ThrowsDomainException()
        {
            // Arrange
            var collection = GivenCollection().WithAnimals().Build();
            var notExistingAnimalId = _fixture.Create<Guid>();

            // Act // Assert
            collection.Invoking(x => x.ReportMolt(notExistingAnimalId))
                  .Should()
                  .Throw<CollectionDomainException>()
                  .WithMessage(string.Format(ExceptionMessages.AnimalNotFound, notExistingAnimalId));
        }
    }
}
