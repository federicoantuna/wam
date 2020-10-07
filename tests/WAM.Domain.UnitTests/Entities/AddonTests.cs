using System;
using System.Linq;
using WAM.Domain.Entities;
using WAM.Domain.Enums;
using WAM.Domain.ValueObjects;
using Xunit;

namespace WAM.Domain.UnitTests.Entities
{
    public class AddonTests
    {
        [Fact]
        public void WhenAddonIsCreated_IdIsSetToARandomGuidAndPropertiesAreSetToMatchingParameters()
        {
            // Arrange
            var name = "Test Addon Name";
            var version = "0.1.2";
            var packageName = "Test Package Name";
            var externalId = 1;
            var package = new Package(packageName, externalId);
            var gameVersionFlavor = GameVersionFlavor.Retail;
            var releaseType = ReleaseType.Stable;

            // Act
            var addon = new Addon(name, version, package, gameVersionFlavor, releaseType);

            // Assert
            Assert.NotEqual(Guid.Empty, addon.Id);
            Assert.Equal(name, addon.Name);
            Assert.Equal(version, addon.Version);
            Assert.Equal(package, addon.Package);
            Assert.Equal(gameVersionFlavor, addon.GameVersionFlavor);
            Assert.Equal(releaseType, addon.ReleaseType);
        }

        [Fact]
        public void AddModule_AddsAModuleToTheAddon()
        {
            // Arrange
            var addonName = "Test Addon Name";
            var version = "0.1.2";
            var packageName = "Test Package Name";
            var externalId = 1;
            var package = new Package(packageName, externalId);
            var gameVersionFlavor = GameVersionFlavor.Retail;
            var releaseType = ReleaseType.Stable;
            var addon = new Addon(addonName, version, package, gameVersionFlavor, releaseType);
            var moduleName = "Test Module Name";
            var module = new Module(moduleName);

            // Act
            addon.AddModule(module);

            // Assert
            _ = Assert.Single(addon.Modules);
            Assert.Equal(module, addon.Modules.Single());
        }

        [Fact]
        public void RemoveModule_RemovesAModuleFromTheAddon()
        {
            // Arrange
            var addonName = "Test Addon Name";
            var version = "0.1.2";
            var packageName = "Test Package Name";
            var externalId = 1;
            var package = new Package(packageName, externalId);
            var gameVersionFlavor = GameVersionFlavor.Retail;
            var releaseType = ReleaseType.Stable;
            var addon = new Addon(addonName, version, package, gameVersionFlavor, releaseType);
            var moduleName = "Test Module Name";
            var module = new Module(moduleName);
            addon.AddModule(module);

            // Act
            addon.RemoveModule(moduleName);

            // Assert
            Assert.Empty(addon.Modules);
        }

        [Fact]
        public void UpdateVersion_UpdatesTheAddonVersionToTheSpecifiedOne()
        {
            // Arrange
            var addonName = "Test Addon Name";
            var version = "0.1.2";
            var packageName = "Test Package Name";
            var externalId = 1;
            var package = new Package(packageName, externalId);
            var gameVersionFlavor = GameVersionFlavor.Retail;
            var releaseType = ReleaseType.Stable;
            var addon = new Addon(addonName, version, package, gameVersionFlavor, releaseType);
            var newVersion = "3.4.5";

            // Act
            addon.UpdateVersion(newVersion);

            // Assert
            Assert.Equal(newVersion, addon.Version);
        }

        [Fact]
        public void UpdateReleaseType_UpdatesTheAddonReleaseTypeToTheSpecifiedOne()
        {
            // Arrange
            var addonName = "Test Addon Name";
            var version = "0.1.2";
            var packageName = "Test Package Name";
            var externalId = 1;
            var package = new Package(packageName, externalId);
            var gameVersionFlavor = GameVersionFlavor.Retail;
            var releaseType = ReleaseType.Stable;
            var addon = new Addon(addonName, version, package, gameVersionFlavor, releaseType);
            var newReleaseType = ReleaseType.Alpha;

            // Act
            addon.UpdateReleaseType(newReleaseType);

            // Assert
            Assert.Equal(newReleaseType, addon.ReleaseType);
        }
    }
}