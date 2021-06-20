using System;
using System.Threading;
using System.Threading.Tasks;
using EightCare.Application.Common.Interfaces;
using EightCare.Domain.Entities;
using MediatR;

namespace EightCare.Application.Keepers.Commands.RegisterKeeper
{
    public class RegisterKeeperCommand : IRequest<Guid>
    {
        public string Name { get; init; }
        public string Email { get; init; }
        public int Age { get; init; }

        public RegisterKeeperCommand(string name, string email, int age)
        {
            Name = name;
            Email = email;
            Age = age;
        }
    }

    public class RegisterKeeperCommandHandler : IRequestHandler<RegisterKeeperCommand, Guid>
    {
        private readonly IKeeperRepository _keeperRepository;

        public RegisterKeeperCommandHandler(IKeeperRepository keeperRepository)
        {
            _keeperRepository = keeperRepository;
        }

        public async Task<Guid> Handle(RegisterKeeperCommand request, CancellationToken cancellationToken)
        {
            var keeper = new Keeper(request.Name, request.Email, request.Age);

            await _keeperRepository.AddAsync(keeper);
            await _keeperRepository.UnitOfWork.SaveChangesAsync();

            return keeper.Id;
        }
    }
}
