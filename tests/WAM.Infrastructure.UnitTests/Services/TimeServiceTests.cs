using System;
using System.Diagnostics.CodeAnalysis;
using WAM.Infrastructure.Services;
using Xunit;

namespace WAM.Infrastructure.UnitTests.Services
{
    [ExcludeFromCodeCoverage]
    public class TimeServiceTests
    {
        private readonly TimeService _timeService;

        public TimeServiceTests()
        {
            this._timeService = new TimeService();
        }

        [Fact]
        public void UtcNow_ReturnsCurrentTime()
        {
            // Arrange
            var now = DateTime.UtcNow;

            // Act
            var result = this._timeService.UtcNow;

            // Assert
            Assert.True(result >= now);
            Assert.True(result < now.AddSeconds(1));
        }
    }
}