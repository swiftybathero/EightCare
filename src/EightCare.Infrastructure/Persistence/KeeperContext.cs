using System.Threading.Tasks;
using EightCare.Application.Common.Interfaces;
using EightCare.Domain.Entities;
using EightCare.Infrastructure.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace EightCare.Infrastructure.Persistence
{
    public class KeeperContext : DbContext, IUnitOfWork
    {
        public DbSet<Keeper> Keepers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new KeeperEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new AnimalEntityTypeConfiguration());
        }

        public async Task SaveChangesAsync()
        {
            await base.SaveChangesAsync();
        }
    }
}
