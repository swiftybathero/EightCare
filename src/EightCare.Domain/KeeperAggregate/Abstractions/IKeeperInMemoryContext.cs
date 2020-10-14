using EightCare.Domain.Common;
using System.Collections.Generic;

namespace EightCare.Domain.KeeperAggregate.Abstractions
{
    public interface IKeeperInMemoryContext : IUnitOfWork
    {
        List<Keeper> Keepers { get; }
    }
}
