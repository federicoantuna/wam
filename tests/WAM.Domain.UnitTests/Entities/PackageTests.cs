using System;
using System.Diagnostics.CodeAnalysis;
using WAM.Domain.Entities;
using Xunit;

namespace WAM.Domain.UnitTests.Entities
{
    [ExcludeFromCodeCoverage]
    public class PackageTests
    {
        [Fact]
        public void WhenPackageIsCreated_IdIsSetToARandomGuidAndNameAndExternalIdAreSetToSpecifiedValues()
        {
            // Arrange
            var name = "Test Package Name";
            var externalId = new Random().Next();

            // Act
            var sut = new Package(name, externalId);

            // Assert
            Assert.NotEqual(Guid.Empty, sut.Id);
            Assert.Equal(name, sut.Name);
            Assert.Equal(externalId, sut.ExternalId);
        }
    }
}