using System;
using WAM.Domain.Bases;

namespace WAM.Domain.Entities
{
    /// <inheritdoc cref="Entity"/>
    /// <summary>
    /// Represents a package that can be associated with one or more <see cref="Addon"/>.
    /// </summary>
    public class Package : Entity
    {
        /// <summary>
        /// Private constructor for Entity Framework
        /// </summary>
        private Package()
        {
        }

        /// <summary>
        /// Initializes a package with the specified name and external id (the id that the source uses to identify this package).
        /// </summary>
        /// <param name="name">The specified name.</param>
        /// <param name="externalId">The specified external id.</param>
        public Package(String name, Int32 externalId)
        {
            this.Name = name;
            this.ExternalId = externalId;
        }

        /// <summary>
        /// Package's name.
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// Package's external id.
        /// </summary>
        public Int32 ExternalId { get; set; }
    }
}