using System;
using EightCare.Domain.Entities;

namespace EightCare.Application.Common.Interfaces
{
    public interface IKeeperRepository : IRepository<Keeper>
    {
        Keeper GetById(Guid keeperId);
        void Add(Keeper keeper);
        void Update(Keeper keeper);
        void Delete(Keeper keeper);
    }
}
