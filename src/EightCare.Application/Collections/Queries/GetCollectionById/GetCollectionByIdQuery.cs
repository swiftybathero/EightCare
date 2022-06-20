using System;
using System.Threading;
using System.Threading.Tasks;
using EightCare.Application.Common.Exceptions;
using EightCare.Application.Common.Interfaces;
using MediatR;

namespace EightCare.Application.Collections.Queries.GetCollectionById
{
    public class GetCollectionByIdQuery : IRequest<CollectionDto>
    {
        public Guid CollectionId { get; init; }

        public GetCollectionByIdQuery(Guid collectionId)
        {
            CollectionId = collectionId;
        }
    }

    public class GetCollectionByIdQueryHandler : IRequestHandler<GetCollectionByIdQuery, CollectionDto>
    {
        private readonly ICollectionRepository _collectionRepository;

        public GetCollectionByIdQueryHandler(ICollectionRepository collectionRepository)
        {
            _collectionRepository = collectionRepository;
        }

        public async Task<CollectionDto> Handle(GetCollectionByIdQuery request, CancellationToken cancellationToken)
        {
            var collection = await _collectionRepository.GetByIdAsync(request.CollectionId);

            if (collection is null)
            {
                throw new EntityNotFoundException($"Collection with Id {request.CollectionId} could not be found.");
            }

            return new CollectionDto
            {
                Id = collection.Id,
                UserId = collection.UserId,
                Name = collection.Name
            };
        }
    }
}
