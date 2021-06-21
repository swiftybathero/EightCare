using System.Reflection;
using System.Threading.Tasks;
using EightCare.Application.Common.Interfaces;
using EightCare.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EightCare.Infrastructure.Persistence
{
    public class KeeperContext : DbContext, IUnitOfWork
    {
        public DbSet<Keeper> Keepers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public async Task SaveChangesAsync()
        {
            await base.SaveChangesAsync();
        }
    }
}
