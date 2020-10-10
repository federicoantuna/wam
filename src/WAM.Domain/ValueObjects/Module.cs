using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using WAM.Domain.Bases;

namespace WAM.Domain.ValueObjects
{
    /// <inheritdoc/>
    /// <summary>
    /// Represents a single module from an <see cref="Entities.Addon"/>.
    /// </summary>
    public class Module : ValueObject
    {
        /// <summary>
        /// Private constructor for Entity Framework
        /// </summary>
        [ExcludeFromCodeCoverage]
        private Module()
        {
        }

        /// <summary>
        /// Initializes a module with the specified name.
        /// </summary>
        /// <param name="name">The specified name.</param>
        public Module(String name)
        {
            this.Name = name;
        }

        /// <summary>
        /// Module's name.
        /// </summary>
        public String Name { get; private set; }

        protected override IEnumerable<Object> GetEqualityComponents()
        {
            yield return this.Name;
        }
    }
}