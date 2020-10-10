using System;

namespace WAM.Infrastructure.Persistence.Common
{
    public static class ColumnName
    {
        #region Common
        public const String Name = nameof(Name);
        #endregion

        #region Packages
        public const String PackageId = nameof(PackageId);
        #endregion

        #region Addons
        public const String AddonId = nameof(AddonId);
        public const String GameVersionFlavor = nameof(GameVersionFlavor);
        public const String ReleaseType = nameof(ReleaseType);
        #endregion

        #region Modules
        public const String ModuleId = nameof(ModuleId);
        #endregion
    }
}