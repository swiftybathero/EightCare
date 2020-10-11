using EightCare.Domain.Common;
using EightCare.Domain.KeeperAggregate;
using EightCare.Domain.KeeperAggregate.Abstractions;
using System;
using System.Linq;

namespace EightCare.Infrastructure.Repositories
{
    public sealed class InMemoryKeeperRepository : IKeeperRepository
    {
        private readonly KeeperInMemoryContext _context;

        public IUnitOfWork UnitOfWork => _context;

        public InMemoryKeeperRepository(KeeperInMemoryContext context)
        {
            _context = context;
        }

        public Keeper GetById(Guid keeperId)
        {
            return _context.Keepers.FirstOrDefault(x => x.Id == keeperId);
        }

        public void Add(Keeper keeper)
        {
            keeper.SetId(Guid.Empty);

            _context.Keepers.Add(keeper);
        }

        public void Update(Keeper keeper)
        {
            if (keeper.Id == Guid.Empty)
                throw new ArgumentException("Keeper without Id cannot be updated.");

            _context.Keepers.Remove(keeper);
            _context.Keepers.Add(keeper);
        }

        public void Delete(Keeper keeper)
        {
            if (keeper.Id == Guid.Empty)
                throw new ArgumentException("Keeper without Id cannot be removed.");

            _context.Keepers.Remove(keeper);
        }
    }
}
