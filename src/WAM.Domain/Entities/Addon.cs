using System;
using System.Collections.Generic;
using System.Linq;
using WAM.Domain.Bases;
using WAM.Domain.Enums;
using WAM.Domain.ValueObjects;

namespace WAM.Domain.Entities
{
    /// <inheritdoc cref="Entity"/>
    /// <summary>
    /// Represents an addon associated with a <see cref="Package"/>.
    /// </summary>
    public class Addon : Entity
    {
        private readonly ICollection<Module> _modules;

        /// <summary>
        /// Private constructor for Entity Framework
        /// </summary>
        private Addon()
        {
        }

        /// <summary>
        /// Initializes the addon with the specified name, version, package, game version flavor and release type.
        /// </summary>
        /// <param name="name">The specified name.</param>
        /// <param name="version">The specified version.</param>
        /// <param name="package">The specified package.</param>
        /// <param name="gameVersionFlavor">The specified game version flavor.</param>
        /// <param name="releaseType">The specified release type.</param>
        public Addon(String name, String version, Package package, GameVersionFlavor gameVersionFlavor, ReleaseType releaseType)
        {
            this.Id = Guid.NewGuid();
            this.Name = name;
            this.Version = version;
            this.Package = package;
            this.GameVersionFlavor = gameVersionFlavor;
            this.ReleaseType = releaseType;

            this._modules = new List<Module>();
        }

        /// <summary>
        /// Addon's name.
        /// </summary>
        public String Name { get; private set; }

        /// <summary>
        /// Addon's version.
        /// </summary>
        public String Version { get; private set; }

        /// <summary>
        /// Addon's game version flavor.
        /// </summary>
        public GameVersionFlavor GameVersionFlavor { get; private set; }

        /// <summary>
        /// Addon's release type.
        /// </summary>
        public ReleaseType ReleaseType { get; private set; }

        /// <summary>
        /// Addon's package.
        /// </summary>
        public Package Package { get; private set; }

        /// <summary>
        /// Addon's modules.
        /// </summary>
        public IEnumerable<Module> Modules => this._modules;

        /// <summary>
        /// Adds a specified <see cref="Module"/> to the addon.
        /// </summary>
        /// <param name="module">The specified module.</param>
        public void AddModule(Module module)
        {
            if (!this._modules.Any(m => m.Name == module.Name))
            {
                this._modules.Add(module);
            }
        }

        /// <summary>
        /// Removes a specified <see cref="Module"/> from the addon.
        /// </summary>
        /// <param name="name">The specified module name.</param>
        public void RemoveModule(String name)
        {
            var module = this._modules.SingleOrDefault(m => m.Name == name);

            if (module != null)
            {
                _ = this._modules.Remove(module);
            }
        }

        /// <summary>
        /// Updates the addon's version to the specified one.
        /// </summary>
        /// <param name="version">The specified version.</param>
        public void UpdateVersion(String version) => this.Version = version;

        /// <summary>
        /// Updates the addon's release type to the specified one.
        /// </summary>
        /// <param name="releaseType">The specified release type.</param>
        public void UpdateReleaseType(ReleaseType releaseType) => this.ReleaseType = releaseType;
    }
}