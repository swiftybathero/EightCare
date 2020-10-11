using EightCare.Domain.KeeperAggregate;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EightCare.Infrastructure.Repositories
{
    public sealed class KeeperUnitOfWork : IKeeperUnitOfWork
    {
        public List<Keeper> Keepers { get; }

        public KeeperUnitOfWork()
        {
            Keepers = new List<Keeper>();
        }

        public void SaveChanges()
        {
            Keepers.Where(x => x.Id == Guid.Empty).ToList()
                   .ForEach(x => x.SetId(Guid.NewGuid()));
        }
    }
}
