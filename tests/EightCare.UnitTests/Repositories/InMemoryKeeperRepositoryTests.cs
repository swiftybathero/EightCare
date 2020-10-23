using AutoFixture;
using AutoFixture.AutoNSubstitute;
using EightCare.Domain.KeeperAggregate;
using EightCare.Domain.KeeperAggregate.Abstractions;
using EightCare.Infrastructure.Repositories;
using EightCare.UnitTests.Extensions;
using FluentAssertions;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace EightCare.UnitTests.Repositories
{
    public class InMemoryKeeperRepositoryTests
    {
        private readonly IFixture _fixture;
        private readonly IKeeperContext _keeperContext;
        private readonly InMemoryKeeperRepository _inMemoryKeeperRepository;

        public InMemoryKeeperRepositoryTests()
        {
            _fixture = new Fixture();
            _fixture.Customize(new AutoNSubstituteCustomization());

            _keeperContext = _fixture.Freeze<IKeeperContext>();
            _inMemoryKeeperRepository = _fixture.Create<InMemoryKeeperRepository>();
        }

        [Fact]
        public void GetById_ExistingElement_ShouldReturnKeeper()
        {
            // Arrange
            var returnedKeepersIds = _fixture.CreateMany<Guid>().ToArray();
            var returnedKeepers = _fixture.CreateManyWithIds<Keeper>(returnedKeepersIds).ToList();
            var expectedKeeper = returnedKeepers.First();

            _keeperContext.Keepers.Returns(returnedKeepers);

            // Act
            var foundKeeper = _inMemoryKeeperRepository.GetById(expectedKeeper.Id);

            // Assert
            foundKeeper.Should().NotBeNull();
            foundKeeper.Should().BeEquivalentTo(expectedKeeper, options => options.ComparingByMembers<Keeper>());
        }

        [Fact]
        public void Add_ShouldAddKeeperToContext()
        {
            // Arrange
            var keeper = _fixture.Create<Keeper>();

            _keeperContext.Keepers.Returns(new List<Keeper>());

            // Act
            _inMemoryKeeperRepository.Add(keeper);

            // Assert
            _keeperContext.Keepers
                                  .Should()
                                  .ContainEquivalentOf(keeper, options => options.ComparingByMembers<Keeper>());
        }

        [Fact]
        public void Update_ExistingKeeper_ShouldUpdateValues()
        {
            // Arrange
            var existingKeeperId = _fixture.Create<Guid>();
            var otherExistingKeeperIds = _fixture.CreateMany<Guid>().ToArray();
            var keeperIds = otherExistingKeeperIds.Concat(new List<Guid> { existingKeeperId }).ToArray();

            var keepers = _fixture.CreateManyWithIds<Keeper>(keeperIds).ToList();
            var newKeeperData = _fixture.CreateManyWithIds<Keeper>(existingKeeperId).First();

            _keeperContext.Keepers.Returns(keepers);

            // Act
            _inMemoryKeeperRepository.Update(newKeeperData);

            // Assert
            _inMemoryKeeperRepository.GetById(existingKeeperId)
                                     .Should()
                                     .BeEquivalentTo(newKeeperData, options => options.ComparingByMembers<Keeper>());
        }

        [Fact]
        public void Update_KeeperWithoutId_ShouldThrowArgumentException()
        {
            // Arrange
            var keeperWithoutId = _fixture.Create<Keeper>();

            // Act // Assert
            _inMemoryKeeperRepository.Invoking(x => x.Update(keeperWithoutId))
                                     .Should()
                                     .Throw<ArgumentException>();
        }

        [Fact]
        public void Delete_ExistingKeeper_ShouldDeleteKeeper()
        {
            // Arrange
            var existingKeeperId = _fixture.Create<Guid>();
            var otherExistingKeeperIds = _fixture.CreateMany<Guid>().ToArray();
            var keeperIds = otherExistingKeeperIds.Concat(new List<Guid> { existingKeeperId }).ToArray();

            var keepers = _fixture.CreateManyWithIds<Keeper>(keeperIds).ToList();
            var keeperToDelete = _fixture.CreateManyWithIds<Keeper>(existingKeeperId).First();

            _keeperContext.Keepers.Returns(keepers);

            // Act
            _inMemoryKeeperRepository.Delete(keeperToDelete);

            // Assert
            _inMemoryKeeperRepository.GetById(existingKeeperId).Should().BeNull();
        }

        [Fact]
        public void Delete_KeeperWithoutId_ShouldThrowArgumentException()
        {
            // Arrange
            var keeperWithoutId = _fixture.Create<Keeper>();

            // Act // Assert
            _inMemoryKeeperRepository.Invoking(x => x.Delete(keeperWithoutId))
                                     .Should()
                                     .Throw<ArgumentException>();
        }
    }
}
