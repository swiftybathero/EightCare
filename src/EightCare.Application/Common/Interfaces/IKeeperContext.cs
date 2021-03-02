using System.Collections.Generic;
using EightCare.Domain.Entities;

namespace EightCare.Application.Common.Interfaces
{
    public interface IKeeperContext : IUnitOfWork
    {
        List<Keeper> Keepers { get; }
    }
}
