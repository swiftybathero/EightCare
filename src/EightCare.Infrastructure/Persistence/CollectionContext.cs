using System.Reflection;
using System.Threading.Tasks;
using EightCare.Application.Common.Interfaces;
using EightCare.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EightCare.Infrastructure.Persistence
{
    public class CollectionContext : DbContext, IUnitOfWork
    {
        public DbSet<Collection> Collections { get; set; }

        public CollectionContext(DbContextOptions<CollectionContext> options) : base(options) { }

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
