using System;
using System.Collections.Generic;
using System.Linq;
using EightCare.Application.Common.Interfaces;
using EightCare.Domain.Entities;

namespace EightCare.Infrastructure.Persistence
{
    public sealed class InMemoryKeeperContext : IKeeperContext
    {
        public List<Keeper> Keepers { get; }

        public InMemoryKeeperContext()
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
