using System;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.AutoNSubstitute;
using EightCare.API.Controllers;
using EightCare.Application.Collections.Commands.RegisterCollection;
using EightCare.Application.Collections.Queries.GetCollectionById;
using MediatR;
using NSubstitute;
using Xunit;

namespace EightCare.API.UnitTests.Controllers
{
    public class CollectionsControllerTests
    {
        private readonly CollectionsController _collectionsController;
        private readonly IMediator _mediator;
        private readonly IFixture _fixture;

        public CollectionsControllerTests()
        {
            _fixture = new Fixture();
            _fixture.Customize(new AutoNSubstituteCustomization());

            _mediator = _fixture.Freeze<IMediator>();
            _collectionsController = new CollectionsController(_fixture.Create<IMediator>());
        }

        [Fact]
        public async Task RegisterCollection_ShouldSendRegisterCommand()
        {
            // Arrange
            var registerCollectionCommand = _fixture.Create<RegisterCollectionCommand>();

            // Act
            await _collectionsController.RegisterCollection(registerCollectionCommand);

            // Assert
            await _mediator.Received(1).Send(Arg.Is(registerCollectionCommand));
        }

        [Fact]
        public async Task GetCollectionById_ShouldSendGetByIdQuery()
        {
            // Arrange
            var collectionId = _fixture.Create<Guid>();

            // Act
            await _collectionsController.GetCollectionById(collectionId);

            // Assert
            await _mediator.Received(1).Send(Arg.Is<GetCollectionByIdQuery>(x => x.CollectionId == collectionId));
        }
    }
}
