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
        public string Name { get; init; }
        public string Email { get; init; }
        public int Age { get; init; }

        public RegisterCollectionCommand(string name, string email, int age)
        {
            Name = name;
            Email = email;
            Age = age;
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
            var collection = new Collection(request.Name, request.Email, request.Age);

            await _collectionRepository.AddAsync(collection);
            await _collectionRepository.UnitOfWork.SaveChangesAsync();

            return collection.Id;
        }
    }
}
