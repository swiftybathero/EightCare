using System;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.AutoNSubstitute;
using EightCare.API.Controllers;
using EightCare.Application.Collections.Commands.DeleteCollection;
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
        public async Task RegisterCollection_SendsRegisterCommand()
        {
            // Arrange
            var registerCollectionCommand = _fixture.Create<RegisterCollectionCommand>();

            // Act
            await _collectionsController.RegisterCollection(registerCollectionCommand);

            // Assert
            await _mediator.Received(1).Send(Arg.Is(registerCollectionCommand));
        }

        [Fact]
        public async Task GetCollectionById_SendsGetByIdQuery()
        {
            // Arrange
            var collectionId = _fixture.Create<Guid>();

            // Act
            await _collectionsController.GetCollectionById(collectionId);

            // Assert
            await _mediator.Received(1).Send(Arg.Is<GetCollectionByIdQuery>(x => x.CollectionId == collectionId));
        }

        [Fact]
        public async Task DeleteCollection_SendsDeleteCollectionCommand()
        {
            // Arrange
            var collectionId = _fixture.Create<Guid>();

            // Act
            await _collectionsController.DeleteCollection(collectionId);

            // Assert
            await _mediator.Received(1).Send(Arg.Is<DeleteCollectionCommand>(x => x.CollectionId == collectionId));
        }
    }
}
