using EightCare.Domain.Common;
using System;

namespace EightCare.Domain.KeeperAggregate.Abstractions
{
    public interface IKeeperRepository : IRepository<Keeper>
    {
        Keeper GetById(Guid keeperId);
        void Add(Keeper keeper);
        void Update(Keeper keeper);
        void Delete(Keeper keeper);
    }
}
