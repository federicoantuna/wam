using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using WAM.Domain.Entities;
using WAM.Domain.Enums;

namespace WAM.Infrastructure.Persistence.Configurations
{
    /// <inheritdoc/>
    /// <summary>
    /// Provides configuration for the persistence of <see cref="Addon"/>.
    /// </summary>
    public class AddonConfiguration : IEntityTypeConfiguration<Addon>
    {
        public void Configure(EntityTypeBuilder<Addon> builder)
        {
            _ = builder.Ignore(a => a.DomainEvents);

            _ = builder.Property(a => a.Name)
                .IsRequired();
            _ = builder.Property(a => a.Version)
                .IsRequired();
            _ = builder.Property(a => a.Modules)
                .IsRequired();
            _ = builder.Property(a => a.GameVersionFlavor)
                .HasConversion(t => (GameVersionFlavor.GameVersionFlavorEnum)t,
                    f => GameVersionFlavor.FromEnum(f))
                .IsRequired();
            _ = builder.Property(a => a.ReleaseType)
                .IsRequired();

            _ = builder.HasIndex("PackageId", "GameVersionFlavor", "ReleaseType")
                .IsUnique();
            _ = builder.HasIndex("Name", "GameVersionFlavor", "ReleaseType")
                .IsUnique();

            _ = builder.HasOne(a => a.Package)
                .WithMany();

            _ = builder.OwnsMany(a => a.Modules, m =>
            {
                _ = m.WithOwner().HasForeignKey("AddonId");
                _ = m.Property<Guid>("ModuleId").HasDefaultValue(Guid.NewGuid());
                _ = m.HasKey("ModuleId");
                _ = m.ToTable("Modules");
            });
        }
    }
}