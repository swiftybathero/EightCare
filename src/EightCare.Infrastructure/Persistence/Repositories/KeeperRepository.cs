using System;
using System.Threading.Tasks;
using EightCare.Application.Common.Interfaces;
using EightCare.Domain.Entities;

namespace EightCare.Infrastructure.Persistence.Repositories
{
    public class KeeperRepository : IKeeperRepository
    {
        private readonly KeeperContext _keeperContext;

        public KeeperRepository(KeeperContext keeperContext)
        {
            _keeperContext = keeperContext;
        }

        public IUnitOfWork UnitOfWork => _keeperContext;

        public async Task<Keeper> GetByIdAsync(Guid keeperId)
        {
            return await _keeperContext.Keepers.FindAsync(keeperId);
        }

        public async Task AddAsync(Keeper keeper)
        {
            await _keeperContext.AddAsync(keeper);
        }

        public Task UpdateAsync(Keeper keeper)
        {
            _keeperContext.Update(keeper);

            return Task.CompletedTask;
        }

        public Task DeleteAsync(Keeper keeper)
        {
            _keeperContext.Keepers.Remove(keeper);

            return Task.CompletedTask;
        }
    }
}
