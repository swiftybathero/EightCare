using System;
using EightCare.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EightCare.Infrastructure.Persistence.EntityConfigurations
{
    public class CollectionEntityTypeConfiguration : IEntityTypeConfiguration<Collection>
    {
        public void Configure(EntityTypeBuilder<Collection> collectionBuilder)
        {
            collectionBuilder.HasKey(c => c.Id);

            collectionBuilder.Property(c => c.UserId)
                             .IsRequired();

            collectionBuilder.HasMany(c => c.Animals)
                             .WithOne();

            collectionBuilder.Property<DateTime>("Created")
                             .HasDefaultValueSql("GETDATE()");

            collectionBuilder.ToTable(nameof(Collection));
        }
    }
}
