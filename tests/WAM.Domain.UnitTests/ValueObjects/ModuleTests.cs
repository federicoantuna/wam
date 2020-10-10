using System.Diagnostics.CodeAnalysis;
using WAM.Domain.ValueObjects;
using Xunit;

namespace WAM.Domain.UnitTests.ValueObjects
{
    [ExcludeFromCodeCoverage]
    public class ModuleTests
    {
        [Fact]
        public void WhenModuleIsCreated_NameIsSetToSpecifiedValue()
        {
            // Arrange
            var name = "Test Module Name";

            // Act
            var module = new Module(name);

            // Assert
            Assert.Equal(name, module.Name);
        }
    }
}