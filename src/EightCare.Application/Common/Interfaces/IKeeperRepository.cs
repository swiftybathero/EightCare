using System;
using EightCare.Domain.Common;

namespace EightCare.Application.Common.Interfaces
{
    public interface IKeeperRepository : IRepository<Domain.KeeperAggregate.Keeper>
    {
        Domain.KeeperAggregate.Keeper GetById(Guid keeperId);
        void Add(Domain.KeeperAggregate.Keeper keeper);
        void Update(Domain.KeeperAggregate.Keeper keeper);
        void Delete(Domain.KeeperAggregate.Keeper keeper);
    }
}
