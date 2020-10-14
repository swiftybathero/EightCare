using AutoFixture;
using AutoFixture.AutoNSubstitute;
using EightCare.Domain.KeeperAggregate;
using EightCare.Domain.KeeperAggregate.Abstractions;
using EightCare.Infrastructure.Repositories;
using EightCare.UnitTests.Extensions;
using FluentAssertions;
using NSubstitute;
using System;
using System.Linq;
using Xunit;

namespace EightCare.UnitTests.Repositories
{
    public class InMemoryKeeperRepositoryTests
    {
        private readonly IFixture _fixture;
        private readonly IKeeperInMemoryContext _keeperInMemoryContext;
        private readonly InMemoryKeeperRepository _inMemoryKeeperRepository;

        public InMemoryKeeperRepositoryTests()
        {
            _fixture = new Fixture();
            _fixture.Customize(new AutoNSubstituteCustomization());

            _keeperInMemoryContext = _fixture.Freeze<IKeeperInMemoryContext>();
            _inMemoryKeeperRepository = _fixture.Create<InMemoryKeeperRepository>();
        }

        [Fact]
        public void GetById_ExistingElement_ShouldReturnKeeper()
        {
            // Arrange
            var returnedKeepersIds = _fixture.CreateMany<Guid>().ToArray();
            var returnedKeepers = _fixture.CreateManyWithIds<Keeper>(returnedKeepersIds).ToList();
            var expectedKeeper = returnedKeepers.First();

            _keeperInMemoryContext.Keepers.Returns(returnedKeepers);

            // Act
            var foundKeeper = _inMemoryKeeperRepository.GetById(expectedKeeper.Id);

            // Assert
            foundKeeper.Should().NotBeNull();
            foundKeeper.Should().BeEquivalentTo(expectedKeeper, options => options.ComparingByMembers<Keeper>());
        }
    }
}
