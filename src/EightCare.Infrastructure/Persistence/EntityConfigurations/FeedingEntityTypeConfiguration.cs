using EightCare.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EightCare.Infrastructure.Persistence.EntityConfigurations
{
    public class FeedingEntityTypeConfiguration : IEntityTypeConfiguration<Feeding>
    {
        public void Configure(EntityTypeBuilder<Feeding> feedingBuilder)
        {
            feedingBuilder.HasKey(f => f.Id);

            feedingBuilder.ToTable(nameof(Feeding));
        }
    }
}
