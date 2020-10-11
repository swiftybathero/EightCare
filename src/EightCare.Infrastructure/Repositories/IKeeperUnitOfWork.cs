using EightCare.Domain.Common;
using EightCare.Domain.KeeperAggregate;
using System.Collections.Generic;

namespace EightCare.Infrastructure.Repositories
{
    public interface IKeeperUnitOfWork : IUnitOfWork
    {
        List<Keeper> Keepers { get; }
    }
}
