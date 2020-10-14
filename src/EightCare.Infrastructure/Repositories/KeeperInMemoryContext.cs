using EightCare.Domain.KeeperAggregate;
using EightCare.Domain.KeeperAggregate.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EightCare.Infrastructure.Repositories
{
    public sealed class KeeperInMemoryContext : IKeeperInMemoryContext
    {
        public List<Keeper> Keepers { get; }

        public KeeperInMemoryContext()
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
