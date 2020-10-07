using System;
using WAM.Domain.Entities;
using Xunit;

namespace WAM.Domain.UnitTests.Entities
{
    public class PackageTests
    {
        [Fact]
        public void WhenPackageIsCreated_IdIsSetToARandomGuidAndNameAndExternalIdAreSetToSpecifiedValues()
        {
            // Arrange
            var name = "Test Package Name";
            var externalId = new Random().Next();

            // Act
            var package = new Package(name, externalId);

            // Assert
            Assert.NotEqual(Guid.Empty, package.Id);
            Assert.Equal(name, package.Name);
            Assert.Equal(externalId, package.ExternalId);
        }
    }
}