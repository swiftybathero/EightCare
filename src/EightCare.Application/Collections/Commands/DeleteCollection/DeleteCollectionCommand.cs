using System;
using System.Threading;
using System.Threading.Tasks;
using EightCare.Application.Common.Interfaces;
using MediatR;

namespace EightCare.Application.Collections.Commands.DeleteCollection
{
    public class DeleteCollectionCommand : IRequest<Unit>
    {
        public Guid CollectionId { get; init; }

        public DeleteCollectionCommand(Guid collectionId)
        {
            CollectionId = collectionId;
        }
    }

    public class DeleteCollectionCommandHandler : IRequestHandler<DeleteCollectionCommand>
    {
        private readonly ICollectionRepository _collectionRepository;

        public DeleteCollectionCommandHandler(ICollectionRepository collectionRepository)
        {
            _collectionRepository = collectionRepository;
        }

        public async Task<Unit> Handle(DeleteCollectionCommand request, CancellationToken cancellationToken)
        {
            await _collectionRepository.DeleteAsync(request.CollectionId);
            await _collectionRepository.UnitOfWork.SaveChangesAsync();

            return Unit.Value;
        }
    }
}