using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.AutoNSubstitute;
using EightCare.Application.Common.Interfaces;
using EightCare.Application.Keepers.Commands.RegisterKeeper;
using EightCare.Domain.Entities;
using NSubstitute;
using Xunit;

namespace EightCare.Application.UnitTests.Keepers.Commands.RegisterKeeper
{
    public class RegisterKeeperCommandTests
    {
        private readonly IFixture _fixture;
        private readonly IKeeperRepository _keeperRepository;
        private readonly RegisterKeeperCommandHandler _handler;

        public RegisterKeeperCommandTests()
        {
            _fixture = new Fixture();
            _fixture.Customize(new AutoNSubstituteCustomization());

            _keeperRepository = _fixture.Freeze<IKeeperRepository>();

            _handler = new RegisterKeeperCommandHandler(_fixture.Create<IKeeperRepository>());
        }

        [Fact]
        public async Task Handle_ShouldAddToRepository()
        {
            // Arrange
            var command = _fixture.Create<RegisterKeeperCommand>();

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            _keeperRepository.Received(1).Add(Arg.Any<Keeper>());
        }
    }
}
