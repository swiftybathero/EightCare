using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.AutoNSubstitute;
using EightCare.Application.Common.Interfaces;
using EightCare.Application.Keepers.Queries.GetKeeperById;
using EightCare.Domain.Entities;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace EightCare.Application.UnitTests.Keepers.Queries.GetKeeperById
{
    public class GetKeeperByIdQueryTests
    {
        private readonly IFixture _fixture;
        private readonly IKeeperRepository _keeperRepository;
        private readonly GetKeeperByIdQueryHandler _handler;

        public GetKeeperByIdQueryTests()
        {
            _fixture = new Fixture();
            _fixture.Customize(new AutoNSubstituteCustomization());

            _keeperRepository = _fixture.Freeze<IKeeperRepository>();

            _handler = new GetKeeperByIdQueryHandler(_fixture.Create<IKeeperRepository>());
        }

        [Fact]
        public async Task Handle_ShouldReturnKeeper()
        {
            // Arrange
            var keeper = _fixture.Create<Keeper>();
            _keeperRepository.GetByIdAsync(Arg.Is(keeper.Id)).Returns(keeper);

            var query = new GetKeeperByIdQuery(keeper.Id);

            // Act
            var keeperDto = await _handler.Handle(query, CancellationToken.None);

            // Assert
            keeperDto.Should().BeEquivalentTo(keeper, options =>
            {
                options.ComparingByMembers<Keeper>();
                options.ExcludingMissingMembers();
                return options;
            });
        }
    }
}
