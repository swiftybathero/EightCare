using System;
using System.Linq;
using AutoFixture;
using EightCare.Domain.Entities;
using EightCare.Domain.Exceptions;
using EightCare.Domain.Properties;
using EightCare.Domain.UnitTests.Common.Builders;
using FluentAssertions;
using Xunit;

namespace EightCare.Domain.UnitTests.Domain
{
    public class CollectionAggregateTests
    {
        private readonly IFixture _fixture;
        private readonly CollectionBuilder _collectionBuilder;

        public CollectionAggregateTests()
        {
            _fixture = new Fixture();
            _collectionBuilder = new CollectionBuilder();
        }

        [Fact]
        public void New_ShouldCreateCollection()
        {
            // Arrange
            var name = _fixture.Create<string>();
            var email = _fixture.Create<string>();
            var age = _fixture.Create<int>();

            // Act
            var collection = new Collection(name, email, age);

            // Assert
            collection.Should().NotBeNull();
            collection.Name.Should().Be(name);
            collection.Email.Should().Be(email);
            collection.Age.Should().Be(age);
        }

        [Fact]
        public void AddNewAnimal_ShouldAddAnimal()
        {
            // Arrange
            var collection = _collectionBuilder.Create();
            var expectedAnimal = _fixture.Create<Animal>();

            // Act
            var createdAnimal = collection.AddNewAnimal
            (
                expectedAnimal.ScientificName,
                expectedAnimal.CommonName,
                expectedAnimal.BuyDate,
                expectedAnimal.BuyAge
            );

            // Assert
            createdAnimal.Should().BeEquivalentTo(expectedAnimal, options => options.ComparingByMembers<Animal>());
            collection.Animals.Should().Contain(createdAnimal);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void AddNewAnimal_NoScientificName_ShouldThrowDomainException(string scientificName)
        {
            // Arrange
            var collection = _collectionBuilder.Create();

            // Act // Assert
            collection.Invoking(x => x.AddNewAnimal
                  (
                      scientificName,
                      _fixture.Create<string>(),
                      _fixture.Create<DateTime>(),
                      _fixture.Create<int>()
                  ))
                  .Should()
                  .Throw<CollectionDomainException>()
                  .WithMessage(ExceptionMessages.ScientificNameCannotBeEmpty);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void AddNewAnimal_InvalidBuyAge_ShouldThrowDomainException(int buyAge)
        {
            // Arrange
            var collection = _collectionBuilder.Create();

            // Act // Assert
            collection.Invoking(x => x.AddNewAnimal
                  (
                      _fixture.Create<string>(),
                      _fixture.Create<string>(),
                      _fixture.Create<DateTime>(),
                      buyAge
                  ))
                  .Should()
                  .Throw<CollectionDomainException>()
                  .WithMessage(ExceptionMessages.BuyAgeCannotBeLowerThanOne);
        }

        [Fact]
        public void FeedAnimal_AnimalExists_ShouldFeedAnimal()
        {
            // Arrange
            const int FeedAmount = 1;

            var existingAnimalId = _fixture.Create<Guid>();
            var feedingDate = _fixture.Create<DateTime>();
            var collection = _collectionBuilder.BuildDefault().WithAnimals(existingAnimalId).Create();
            var animal = collection.Animals.First(x => x.Id == existingAnimalId);

            // Act
            collection.FeedAnimal(existingAnimalId, FeedAmount, feedingDate);

            // Assert
            animal.Feedings.Should().ContainSingle(x => x.Date == feedingDate && x.Amount == FeedAmount);
        }

        [Fact]
        public void FeedAnimal_NoParameters_ShouldFeedAnimalWithDefaultValues()
        {
            // Arrange
            const int ExpectedDefaultAmount = 1;

            var existingAnimalId = _fixture.Create<Guid>();
            var collection = _collectionBuilder.BuildDefault().WithAnimals(existingAnimalId).Create();
            var animal = collection.Animals.First(x => x.Id == existingAnimalId);

            // Act
            collection.FeedAnimal(existingAnimalId);

            // Assert
            animal.Feedings.Should().ContainSingle(x => x.Date != default && x.Amount == ExpectedDefaultAmount);
        }

        [Fact]
        public void FeedAnimal_AnimalDoesNotExist_ShouldThrowException()
        {
            // Arrange
            var collection = _collectionBuilder.BuildDefault().WithAnimals().Create();
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
        public void FeedAnimal_AmountLowerThanOne_ShouldThrowException(int amount)
        {
            // Arrange
            var existingAnimalId = _fixture.Create<Guid>();
            var collection = _collectionBuilder.BuildDefault().WithAnimals(existingAnimalId).Create();

            // Act // Assert
            collection.Invoking(x => x.FeedAnimal(existingAnimalId, amount))
                  .Should()
                  .Throw<CollectionDomainException>()
                  .WithMessage(ExceptionMessages.FeedAmountCannotBeLowerThanOne);
        }

        [Fact]
        public void ReportMolt_ShouldReportMolt()
        {
            // Arrange
            var existingAnimalId = _fixture.Create<Guid>();
            var collection = _collectionBuilder.BuildDefault().WithAnimals(existingAnimalId).Create();

            var moltingDate = _fixture.Create<DateTime>();
            var moltingAnimal = collection.Animals.First(x => x.Id == existingAnimalId);

            // Act
            collection.ReportMolt(existingAnimalId, moltingDate);

            // Assert
            moltingAnimal.Molts.Should().Contain(x => x.Date == moltingDate);
        }

        [Fact]
        public void ReportMolt_NoDate_ShouldReportDefaultDate()
        {
            // Arrange
            var existingAnimalId = _fixture.Create<Guid>();
            var collection = _collectionBuilder.BuildDefault().WithAnimals(existingAnimalId).Create();

            var moltingAnimal = collection.Animals.First(x => x.Id == existingAnimalId);

            // Act
            collection.ReportMolt(existingAnimalId);

            // Assert
            moltingAnimal.Molts.Should().NotContain(x => x.Date == default);
        }

        [Fact]
        public void ReportMolt_AnimalDoesNotExist_ShouldThrowException()
        {
            // Arrange
            var collection = _collectionBuilder.BuildDefault().WithAnimals().Create();
            var notExistingAnimalId = _fixture.Create<Guid>();

            // Act // Assert
            collection.Invoking(x => x.ReportMolt(notExistingAnimalId))
                  .Should()
                  .Throw<CollectionDomainException>()
                  .WithMessage(string.Format(ExceptionMessages.AnimalNotFound, notExistingAnimalId));
        }
    }
}
