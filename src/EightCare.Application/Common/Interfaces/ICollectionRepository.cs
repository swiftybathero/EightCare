using System;
using System.Threading.Tasks;
using EightCare.Domain.Entities;

namespace EightCare.Application.Common.Interfaces
{
    public interface ICollectionRepository : IRepository<Collection>
    {
        Task<Collection> GetByIdAsync(Guid collectionId);
        Task AddAsync(Collection collection);
        Task DeleteAsync(Collection collection);
    }
}
