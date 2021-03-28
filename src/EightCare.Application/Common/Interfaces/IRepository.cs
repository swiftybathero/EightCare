using EightCare.Domain.Common;

namespace EightCare.Application.Common.Interfaces
{
    public interface IRepository<T> where T : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
