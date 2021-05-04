using System;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.AutoNSubstitute;
using EightCare.API.Controllers;
using EightCare.Application.Keepers.Commands.RegisterKeeper;
using EightCare.Application.Keepers.Queries.GetKeeperById;
using MediatR;
using NSubstitute;
using Xunit;

namespace EightCare.API.UnitTests.Controllers
{
    public class KeepersControllerTests
    {
        private readonly KeepersController _keepersController;
        private readonly IMediator _mediator;
        private readonly IFixture _fixture;

        public KeepersControllerTests()
        {
            _fixture = new Fixture();
            _fixture.Customize(new AutoNSubstituteCustomization());

            _mediator = _fixture.Freeze<IMediator>();
            _keepersController = new KeepersController(_fixture.Create<IMediator>());
        }

        [Fact]
        public async Task RegisterKeeper_ShouldSendRegisterCommand()
        {
            // Arrange
            var registerKeeperCommand = _fixture.Create<RegisterKeeperCommand>();

            // Act
            await _keepersController.RegisterKeeper(registerKeeperCommand);

            // Assert
            await _mediator.Received(1).Send(Arg.Is(registerKeeperCommand));
        }

        [Fact]
        public async Task GetKeeperById_ShouldSendGetByIdQuery()
        {
            // Arrange
            var keeperId = _fixture.Create<Guid>();

            // Act
            await _keepersController.GetKeeperById(keeperId);

            // Assert
            await _mediator.Received(1).Send(Arg.Is<GetKeeperByIdQuery>(x => x.KeeperId == keeperId));
        }
    }
}
