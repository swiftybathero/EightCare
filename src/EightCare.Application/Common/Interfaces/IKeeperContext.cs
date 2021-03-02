using System.Collections.Generic;
using EightCare.Domain.Common;

namespace EightCare.Application.Common.Interfaces
{
    public interface IKeeperContext : IUnitOfWork
    {
        List<Domain.KeeperAggregate.Keeper> Keepers { get; }
    }
}
