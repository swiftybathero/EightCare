using EightCare.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EightCare.Infrastructure.EntityConfigurations
{
    public class KeeperEntityTypeConfiguration : IEntityTypeConfiguration<Keeper>
    {
        public void Configure(EntityTypeBuilder<Keeper> keeperBuilder)
        {
            keeperBuilder.HasKey(k => k.Id);

            keeperBuilder.Property(k => k.Email)
                         .IsRequired();

            keeperBuilder.HasMany(k => k.Animals)
                         .WithOne();
        }
    }
}
