using System.Diagnostics.CodeAnalysis;
using WAM.Infrastructure.Abstractions;
using WAM.Infrastructure.Factories;
using Xunit;

namespace WAM.Infrastructure.UnitTests.Factories
{
    [ExcludeFromCodeCoverage]
    public class SystemWebClientFactoryTests
    {
        private readonly SystemWebClientFactory _sut;

        public SystemWebClientFactoryTests()
        {
            this._sut = new SystemWebClientFactory();
        }

        [Fact]
        public void Create_ReturnsSystemWebClient()
        {
            // Arrange

            // Act
            var result = this._sut.Create();

            // Assert
            Assert.NotNull(result);
            _ = Assert.IsType<SystemWebClient>(result);
        }
    }
}