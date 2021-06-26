using System;
using System.Reflection;
using System.Threading.Tasks;
using EightCare.Application.Common.Interfaces;
using EightCare.Domain.Entities;
using EightCare.Infrastructure.Common.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace EightCare.Infrastructure.Persistence
{
    public class KeeperContext : DbContext, IUnitOfWork
    {
        private readonly DatabaseConfiguration _configuration;

        public DbSet<Keeper> Keepers { get; set; }

        public KeeperContext(IOptions<DatabaseConfiguration> configuration)
        {
            _configuration = configuration.Value;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public async Task SaveChangesAsync()
        {
            await base.SaveChangesAsync();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.ConnectionString)
                          .EnableSensitiveDataLogging() // TODO: Temporary logging
                          .LogTo(Console.WriteLine);
        }
    }
}
