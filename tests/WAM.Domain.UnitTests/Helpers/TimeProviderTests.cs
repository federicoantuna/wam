using System;
using WAM.Domain.Helpers;
using Xunit;

namespace WAM.Domain.UnitTests.Helpers
{
    public class TimeProviderTests
    {
        [Fact]
        public void SetFixedTime_ForcesTheTimeProviderToReturnASpecificDate()
        {
            // Arrange
            TimeProvider.SetRealTime();
            var specificDate = DateTime.UtcNow.AddDays(new Random().Next(1, 365));

            // Act
            TimeProvider.SetFixedTime(specificDate);

            // Assert
            Assert.Equal(specificDate, TimeProvider.UtcNow);
        }

        [Fact]
        public void SetRealTime_ForcesTheTimeProviderToReturnTheCUrrentDate()
        {
            // Arrange
            var futureDate = DateTime.UtcNow.AddDays(new Random().Next(1, 365));
            TimeProvider.SetFixedTime(futureDate);
            var now = DateTime.UtcNow;

            // Act
            TimeProvider.SetRealTime();

            // Assert
            Assert.True(TimeProvider.UtcNow >= now);
            Assert.True(TimeProvider.UtcNow < futureDate);
        }
    }
}