using System;
using System.Diagnostics.CodeAnalysis;
using WAM.Domain.Bases;

namespace WAM.Domain.Entities
{
    /// <inheritdoc/>
    /// <summary>
    /// Represents a package that can be associated with one or more <see cref="Addon"/>.
    /// </summary>
    public class Package : Entity
    {
        /// <summary>
        /// Private constructor for Entity Framework
        /// </summary>
        // ExcludeFromCodeCoverage: There is no value in testing this.
        [ExcludeFromCodeCoverage]
        private Package()
        {
        }

        /// <summary>
        /// Initializes a package with the specified name and external id (the id that the source uses to identify this package).
        /// </summary>
        /// <param name="name">The specified name.</param>
        /// <param name="externalId">The specified external id.</param>
        public Package(Int32 externalId, String name)
        {
            this.ExternalId = externalId;
            this.Name = name;
        }

        /// <summary>
        /// Package's external id.
        /// </summary>
        public Int32 ExternalId { get; private set; }

        /// <summary>
        /// Package's name.
        /// </summary>
        public String Name { get; private set; }
    }
}