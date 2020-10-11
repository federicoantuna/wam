using System;

namespace WAM.Infrastructure.Persistence.Common
{
    /// <summary>
    /// Provides the name of the columns for different tables on the database.
    /// </summary>
    public static class ColumnName
    {
        #region Common
        /// <summary>
        /// Column Name
        /// </summary>
        public const String Name = nameof(Name);
        #endregion

        #region Packages
        /// <summary>
        /// Column PackageId
        /// </summary>
        public const String PackageId = nameof(PackageId);
        #endregion

        #region Addons
        /// <summary>
        /// Column AddonId
        /// </summary>
        public const String AddonId = nameof(AddonId);

        /// <summary>
        /// Column GameVersionFlavor
        /// </summary>
        public const String GameVersionFlavor = nameof(GameVersionFlavor);

        /// <summary>
        /// Column ReleaseType
        /// </summary>
        public const String ReleaseType = nameof(ReleaseType);
        #endregion

        #region Modules
        /// <summary>
        /// Column ModuleId
        /// </summary>
        public const String ModuleId = nameof(ModuleId);
        #endregion
    }
}