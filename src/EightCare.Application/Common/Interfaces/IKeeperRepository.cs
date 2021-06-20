using System;
using System.Threading.Tasks;
using EightCare.Domain.Entities;

namespace EightCare.Application.Common.Interfaces
{
    public interface IKeeperRepository : IRepository<Keeper>
    {
        Task<Keeper> GetByIdAsync(Guid keeperId);
        Task AddAsync(Keeper keeper);
        Task UpdateAsync(Keeper keeper);
        Task DeleteAsync(Keeper keeper);
    }
}
