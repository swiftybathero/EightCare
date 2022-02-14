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
            collectionBuilder.HasKey(k => k.Id);

            collectionBuilder.Property(k => k.Email)
                         .IsRequired();

            collectionBuilder.HasMany(k => k.Animals)
                         .WithOne();

            collectionBuilder.Property<DateTime>("Created")
                         .HasDefaultValueSql("GETDATE()");

            collectionBuilder.ToTable(nameof(Collection));
        }
    }
}
