using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WAM.Domain.Entities;

namespace WAM.Infrastructure.Persistence.Configurations
{
    /// <inheritdoc/>
    /// <summary>
    /// Provides configuration for the persistence of <see cref="Package"/>.
    /// </summary>
    public class PackageConfiguration : IEntityTypeConfiguration<Package>
    {
        public void Configure(EntityTypeBuilder<Package> builder)
        {
            _ = builder.Ignore(p => p.DomainEvents);

            _ = builder.Property(p => p.Name)
                .IsRequired();

            _ = builder.HasIndex(p => p.Name)
                .IsUnique();
        }
    }
}