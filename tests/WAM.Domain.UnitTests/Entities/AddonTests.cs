using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using WAM.Domain.Entities;
using WAM.Domain.Enums;
using WAM.Domain.ValueObjects;
using Xunit;

namespace WAM.Domain.UnitTests.Entities
{
    [ExcludeFromCodeCoverage]
    public class AddonTests
    {
        [Fact]
        public void WhenAddonIsCreated_IdIsSetToARandomGuidAndPropertiesAreSetToMatchingParameters()
        {
            // Arrange
            var packageName = "Test Package Name";
            var externalId = 1;
            var name = "Test Addon Name";
            var version = "0.1.2";
            var package = new Package(externalId, packageName);
            var gameVersionFlavor = GameVersionFlavor.Retail;
            var releaseType = ReleaseType.Stable;

            // Act
            var sut = new Addon(name, version, package, gameVersionFlavor, releaseType);

            // Assert
            Assert.NotEqual(Guid.Empty, sut.Id);
            Assert.Equal(name, sut.Name);
            Assert.Equal(version, sut.Version);
            Assert.Equal(package, sut.Package);
            Assert.Equal(gameVersionFlavor, sut.GameVersionFlavor);
            Assert.Equal(releaseType, sut.ReleaseType);
        }

        [Fact]
        public void AddModule_AddsAModuleToTheAddon()
        {
            // Arrange
            var packageName = "Test Package Name";
            var externalId = 1;
            var moduleName = "Test Module Name";
            var name = "Test Addon Name";
            var version = "0.1.2";
            var package = new Package(externalId, packageName);
            var gameVersionFlavor = GameVersionFlavor.Retail;
            var releaseType = ReleaseType.Stable;
            var module = new Module(moduleName);
            
            var sut = new Addon(name, version, package, gameVersionFlavor, releaseType);

            // Act
            sut.AddModule(module);

            // Assert
            _ = Assert.Single(sut.Modules);
            Assert.Equal(module, sut.Modules.Single());
        }

        [Fact]
        public void AddModule_DoesNotAddAModuleToTheAddonIfAlreadyThere()
        {
            // Arrange
            var packageName = "Test Package Name";
            var externalId = 1;
            var moduleName = "Test Module Name";
            var name = "Test Addon Name";
            var version = "0.1.2";
            var package = new Package(externalId, packageName);
            var gameVersionFlavor = GameVersionFlavor.Retail;
            var releaseType = ReleaseType.Stable;
            var module = new Module(moduleName);
            
            var sut = new Addon(name, version, package, gameVersionFlavor, releaseType);
            sut.AddModule(module);

            // Act
            sut.AddModule(module);

            // Assert
            _ = Assert.Single(sut.Modules);
            Assert.Equal(module, sut.Modules.Single());
        }

        [Fact]
        public void AddModules_AddsModulesToTheAddon()
        {
            // Arrange
            var packageName = "Test Package Name";
            var externalId = 1;
            var moduleAName = "Test Module A Name";
            var moduleBName = "Test Module B Name";
            var name = "Test Addon Name";
            var version = "0.1.2";
            var package = new Package(externalId, packageName);
            var gameVersionFlavor = GameVersionFlavor.Retail;
            var releaseType = ReleaseType.Stable;
            var moduleA = new Module(moduleAName);
            var moduleB = new Module(moduleBName);

            var sut = new Addon(name, version, package, gameVersionFlavor, releaseType);

            var modules = new Module[]
            {
                moduleA,
                moduleB
            };

            // Act
            sut.AddModules(modules);

            // Assert
            Assert.Collection(sut.Modules,
                item => Assert.Equal(moduleAName, item.Name),
                item => Assert.Equal(moduleBName, item.Name));
        }

        [Fact]
        public void AddModules_DoesNotAddModulesToTheAddonIfAlreadyThere()
        {
            // Arrange
            var packageName = "Test Package Name";
            var externalId = 1;
            var moduleAName = "Test Module A Name";
            var moduleBName = "Test Module B Name";
            var name = "Test Addon Name";
            var version = "0.1.2";
            var package = new Package(externalId, packageName);
            var gameVersionFlavor = GameVersionFlavor.Retail;
            var releaseType = ReleaseType.Stable;
            var moduleA = new Module(moduleAName);
            var moduleB = new Module(moduleBName);

            var sut = new Addon(name, version, package, gameVersionFlavor, releaseType);
            sut.AddModule(moduleA);

            var modules = new Module[]
            {
                moduleA,
                moduleB
            };

            // Act
            sut.AddModules(modules);

            // Assert
            Assert.Collection(sut.Modules,
                item => Assert.Equal(moduleAName, item.Name),
                item => Assert.Equal(moduleBName, item.Name));
        }

        [Fact]
        public void RemoveModule_RemovesAModuleFromTheAddon()
        {
            // Arrange
            var packageName = "Test Package Name";
            var externalId = 1;
            var moduleName = "Test Module Name";
            var name = "Test Addon Name";
            var version = "0.1.2";
            var package = new Package(externalId, packageName);
            var gameVersionFlavor = GameVersionFlavor.Retail;
            var releaseType = ReleaseType.Stable;
            var module = new Module(moduleName);
            
            var sut = new Addon(name, version, package, gameVersionFlavor, releaseType);
            sut.AddModule(module);

            // Act
            sut.RemoveModule(moduleName);

            // Assert
            Assert.Empty(sut.Modules);
        }

        [Fact]
        public void RemoveModule_DoesNothingIfModuleIsNotInTheAddon()
        {
            // Arrange
            var packageName = "Test Package Name";
            var externalId = 1;
            var name = "Test Addon Name";
            var version = "0.1.2";
            var package = new Package(externalId, packageName);
            var gameVersionFlavor = GameVersionFlavor.Retail;
            var releaseType = ReleaseType.Stable;
            var moduleName = "Test Module Name";

            var sut = new Addon(name, version, package, gameVersionFlavor, releaseType);

            // Act
            sut.RemoveModule(moduleName);

            // Assert
            Assert.Empty(sut.Modules);
        }

        [Fact]
        public void RemoveModules_RemovesModulesFromTheAddon()
        {
            // Arrange
            var packageName = "Test Package Name";
            var externalId = 1;
            var moduleAName = "Test Module A Name";
            var moduleBName = "Test Module B Name";
            var name = "Test Addon Name";
            var version = "0.1.2";
            var package = new Package(externalId, packageName);
            var gameVersionFlavor = GameVersionFlavor.Retail;
            var releaseType = ReleaseType.Stable;
            var moduleA = new Module(moduleAName);
            var moduleB = new Module(moduleBName);

            var sut = new Addon(name, version, package, gameVersionFlavor, releaseType);
            sut.AddModule(moduleA);
            sut.AddModule(moduleB);

            var modules = new String[]
            {
                moduleAName,
                moduleBName
            };

            // Act
            sut.RemoveModules(modules);

            // Assert
            Assert.Empty(sut.Modules);
        }

        [Fact]
        public void RemoveModules_DoesNothingIfModulesAreNotInTheAddon()
        {
            // Arrange
            var packageName = "Test Package Name";
            var externalId = 1;
            var moduleAName = "Test Module A Name";
            var moduleBName = "Test Module B Name";
            var name = "Test Addon Name";
            var version = "0.1.2";
            var package = new Package(externalId, packageName);
            var gameVersionFlavor = GameVersionFlavor.Retail;
            var releaseType = ReleaseType.Stable;

            var sut = new Addon(name, version, package, gameVersionFlavor, releaseType);

            var modules = new String[]
            {
                moduleAName,
                moduleBName
            };

            // Act
            sut.RemoveModules(modules);

            // Assert
            Assert.Empty(sut.Modules);
        }

        [Fact]
        public void UpdateName_UpdatesTheAddonNameToTheSpecifiedOne()
        {
            // Arrange
            var packageName = "Test Package Name";
            var externalId = 1;
            var name = "Test Addon Name";
            var version = "0.1.2";
            var package = new Package(externalId, packageName);
            var gameVersionFlavor = GameVersionFlavor.Retail;
            var releaseType = ReleaseType.Stable;
            var newName = "New Addon Name";

            var sut = new Addon(name, version, package, gameVersionFlavor, releaseType);

            // Act
            sut.UpdateName(newName);

            // Assert
            Assert.Equal(newName, sut.Name);
        }

        [Fact]
        public void UpdateVersion_UpdatesTheAddonVersionToTheSpecifiedOne()
        {
            // Arrange
            var packageName = "Test Package Name";
            var externalId = 1;
            var name = "Test Addon Name";
            var version = "0.1.2";
            var package = new Package(externalId, packageName);
            var gameVersionFlavor = GameVersionFlavor.Retail;
            var releaseType = ReleaseType.Stable;
            var newVersion = "3.4.5";
            
            var sut = new Addon(name, version, package, gameVersionFlavor, releaseType);

            // Act
            sut.UpdateVersion(newVersion);

            // Assert
            Assert.Equal(newVersion, sut.Version);
        }

        [Fact]
        public void UpdateReleaseType_UpdatesTheAddonReleaseTypeToTheSpecifiedOne()
        {
            // Arrange
            var packageName = "Test Package Name";
            var externalId = 1;
            var name = "Test Addon Name";
            var version = "0.1.2";
            var package = new Package(externalId, packageName);
            var gameVersionFlavor = GameVersionFlavor.Retail;
            var releaseType = ReleaseType.Stable;
            var newReleaseType = ReleaseType.Alpha;

            var sut = new Addon(name, version, package, gameVersionFlavor, releaseType);

            // Act
            sut.UpdateReleaseType(newReleaseType);

            // Assert
            Assert.Equal(newReleaseType, sut.ReleaseType);
        }
    }
}