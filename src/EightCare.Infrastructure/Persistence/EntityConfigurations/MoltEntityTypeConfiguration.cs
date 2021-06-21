using EightCare.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EightCare.Infrastructure.Persistence.EntityConfigurations
{
    public class MoltEntityTypeConfiguration : IEntityTypeConfiguration<Molt>
    {
        public void Configure(EntityTypeBuilder<Molt> moltBuilder)
        {
            moltBuilder.HasKey(m => m.Id);

            moltBuilder.ToTable(nameof(Molt));
        }
    }
}
