using System.Threading.Tasks;

namespace EightCare.Application.Common.Interfaces
{
    public interface IUnitOfWork
    {
        Task SaveChangesAsync();
    }
}
