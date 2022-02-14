using System;
using System.Threading;
using System.Threading.Tasks;
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

            // TODO: Fix with proper exception/result
            return collection is not null ? new CollectionDto
            {
                Id = collection.Id,
                Name = collection.Name,
                Email = collection.Email,
                Age = collection.Age
            } : null;
        }
    }
}
