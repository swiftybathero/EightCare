using System;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.AutoNSubstitute;
using EightCare.Application.Collections.Commands.DeleteCollection;
using EightCare.Application.Common.Interfaces;
using NSubstitute;
using Xunit;

namespace EightCare.Application.UnitTests.Collections.Commands.DeleteCollection
{
    public class DeleteCollectionCommandTests
    {
        private readonly IFixture _fixture;
        private readonly ICollectionRepository _collectionRepository;
        private readonly DeleteCollectionCommandHandler _handler;

        public DeleteCollectionCommandTests()
        {
            _fixture = new Fixture();
            _fixture.Customize(new AutoNSubstituteCustomization());

            _collectionRepository = _fixture.Freeze<ICollectionRepository>();

            _handler = new DeleteCollectionCommandHandler(_fixture.Create<ICollectionRepository>());
        }

        [Fact]
        public async Task Handle_DeletesUsingRepository()
        {
            // Arrange
            var command = _fixture.Create<DeleteCollectionCommand>();

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            await _collectionRepository.Received(1).DeleteAsync(Arg.Any<Guid>());
            await _collectionRepository.UnitOfWork.Received(1).SaveChangesAsync();
        }
    }
}
