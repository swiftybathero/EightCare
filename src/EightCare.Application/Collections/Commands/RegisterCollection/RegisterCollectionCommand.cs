using System;
using System.Threading;
using System.Threading.Tasks;
using EightCare.Application.Common.Interfaces;
using EightCare.Domain.Entities;
using MediatR;

namespace EightCare.Application.Collections.Commands.RegisterCollection
{
    public class RegisterCollectionCommand : IRequest<Guid>
    {
        public Guid UserId { get; init; }
        public string Name { get; init; }

        public RegisterCollectionCommand(Guid userId, string name)
        {
            UserId = userId;
            Name = name;
        }
    }

    public class RegisterCollectionCommandHandler : IRequestHandler<RegisterCollectionCommand, Guid>
    {
        private readonly ICollectionRepository _collectionRepository;

        public RegisterCollectionCommandHandler(ICollectionRepository collectionRepository)
        {
            _collectionRepository = collectionRepository;
        }

        public async Task<Guid> Handle(RegisterCollectionCommand request, CancellationToken cancellationToken)
        {
            var collection = new Collection(request.UserId, request.Name);

            await _collectionRepository.AddAsync(collection);
            await _collectionRepository.UnitOfWork.SaveChangesAsync();

            return collection.Id;
        }
    }
}
