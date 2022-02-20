using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.AutoNSubstitute;
using EightCare.Application.Collections.Commands.RegisterCollection;
using EightCare.Application.Common.Interfaces;
using EightCare.Domain.Entities;
using NSubstitute;
using Xunit;

namespace EightCare.Application.UnitTests.Collections.Commands.RegisterCollection
{
    public class RegisterCollectionCommandTests
    {
        private readonly IFixture _fixture;
        private readonly ICollectionRepository _collectionRepository;
        private readonly RegisterCollectionCommandHandler _handler;

        public RegisterCollectionCommandTests()
        {
            _fixture = new Fixture();
            _fixture.Customize(new AutoNSubstituteCustomization());

            _collectionRepository = _fixture.Freeze<ICollectionRepository>();

            _handler = new RegisterCollectionCommandHandler(_fixture.Create<ICollectionRepository>());
        }

        [Fact]
        public async Task Handle_AddsToRepository()
        {
            // Arrange
            var command = _fixture.Create<RegisterCollectionCommand>();

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            await _collectionRepository.Received(1).AddAsync(Arg.Any<Collection>());
            await _collectionRepository.UnitOfWork.Received(1).SaveChangesAsync();
        }
    }
}
