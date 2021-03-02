using System;
using System.Threading;
using System.Threading.Tasks;
using EightCare.Domain.KeeperAggregate.Abstractions;
using MediatR;

namespace EightCare.Application.Keeper.Queries.GetKeeperById
{
    public class GetKeeperByIdQuery : IRequest<KeeperDto>
    {
        public Guid KeeperId { get; }

        public GetKeeperByIdQuery(Guid keeperId)
        {
            KeeperId = keeperId;
        }
    }

    public class GetKeeperByIdQueryHandler : IRequestHandler<GetKeeperByIdQuery, KeeperDto>
    {
        private readonly IKeeperRepository _keeperRepository;

        public GetKeeperByIdQueryHandler(IKeeperRepository keeperRepository)
        {
            _keeperRepository = keeperRepository;
        }

        public Task<KeeperDto> Handle(GetKeeperByIdQuery request, CancellationToken cancellationToken)
        {
            var keeper = _keeperRepository.GetById(request.KeeperId);

            return Task.FromResult(new KeeperDto
            {
                Id = keeper.Id,
                Name = keeper.Name,
                Email = keeper.Email,
                Age = keeper.Age
            });
        }
    }
}
