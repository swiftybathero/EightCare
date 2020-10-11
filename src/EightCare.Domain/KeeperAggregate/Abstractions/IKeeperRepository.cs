using EightCare.Domain.Common;

namespace EightCare.Domain.KeeperAggregate.Abstractions
{
    public interface IKeeperRepository : IRepository<Keeper>
    {
        Keeper GetById(int keeperId);
        void Add(Keeper keeper);
        void Update(Keeper keeper);
        void Delete(Keeper keeper);
    }
}
