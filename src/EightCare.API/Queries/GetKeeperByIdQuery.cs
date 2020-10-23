using EightCare.API.Models;
using MediatR;
using System;

namespace EightCare.API.Queries
{
    public class GetKeeperByIdQuery : IRequest<KeeperModel>
    {
        public Guid KeeperId { get; }

        public GetKeeperByIdQuery(Guid keeperId)
        {
            KeeperId = keeperId;
        }
    }
}
