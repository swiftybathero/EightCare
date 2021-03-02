using System;
using System.Threading;
using System.Threading.Tasks;
using EightCare.Application.Common.Interfaces;
using MediatR;

namespace EightCare.Application.Keeper.Commands.RegisterKeeper
{
    public class RegisterKeeperCommand : IRequest<Guid>
    {
        public string Name { get; }
        public string Email { get; }
        public int Age { get; }

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

        public Task<Guid> Handle(RegisterKeeperCommand request, CancellationToken cancellationToken)
        {
            var keeper = new Domain.KeeperAggregate.Keeper(request.Name, request.Email, request.Age);

            _keeperRepository.Add(keeper);
            _keeperRepository.UnitOfWork.SaveChanges();

            return Task.FromResult(keeper.Id);
        }
    }
}
