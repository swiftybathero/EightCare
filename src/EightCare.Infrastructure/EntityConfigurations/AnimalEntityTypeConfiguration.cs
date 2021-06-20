using EightCare.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EightCare.Infrastructure.EntityConfigurations
{
    public class AnimalEntityTypeConfiguration : IEntityTypeConfiguration<Animal>
    {
        public void Configure(EntityTypeBuilder<Animal> animalBuilder)
        {
            animalBuilder.HasKey(a => a.Id);

            animalBuilder.HasMany(a => a.Feedings)
                         .WithOne();

            animalBuilder.HasMany(a => a.Molts)
                         .WithOne();
        }
    }
}
