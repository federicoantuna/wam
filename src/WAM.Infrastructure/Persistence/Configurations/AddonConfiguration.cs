using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Diagnostics.CodeAnalysis;
using WAM.Domain.Entities;
using WAM.Domain.Enums;
using WAM.Domain.ValueObjects;
using WAM.Infrastructure.Persistence.Common;

namespace WAM.Infrastructure.Persistence.Configurations
{
    /// <inheritdoc/>
    /// <summary>
    /// Provides configuration for the persistence of <see cref="Addon"/>.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class AddonConfiguration : IEntityTypeConfiguration<Addon>
    {
        public void Configure(EntityTypeBuilder<Addon> builder)
        {
            _ = builder.Ignore(a => a.DomainEvents);

            _ = builder.Property(a => a.Name).IsRequired();
            _ = builder.Property(a => a.Version).IsRequired();
            _ = builder.Property(a => a.GameVersionFlavor).HasConversion(t => (GameVersionFlavor.GameVersionFlavorEnum)t, f => GameVersionFlavor.FromEnum(f)).IsRequired();
            _ = builder.Property(a => a.ReleaseType).HasConversion(t => (ReleaseType.ReleaseTypeEnum)t, f => ReleaseType.FromEnum(f)).IsRequired();

            _ = builder.HasIndex(ColumnName.PackageId, ColumnName.GameVersionFlavor, ColumnName.ReleaseType).IsUnique();
            _ = builder.HasIndex(ColumnName.Name, ColumnName.GameVersionFlavor, ColumnName.ReleaseType).IsUnique();

            _ = builder.HasOne(a => a.Package).WithMany();

            _ = builder.OwnsMany(a => a.Modules, m =>
            {
                _ = m.WithOwner().HasForeignKey(ColumnName.AddonId);
                _ = m.Property<Guid>(ColumnName.ModuleId).HasDefaultValue(Guid.NewGuid());
                _ = m.HasKey(ColumnName.ModuleId);
                _ = m.ToTable(TableName.Modules);
            });
        }
    }
}