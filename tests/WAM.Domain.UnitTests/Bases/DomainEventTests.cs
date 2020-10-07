using System;
using WAM.Domain.Helpers;
using WAM.Domain.UnitTests.Fakes;
using Xunit;

namespace WAM.Domain.UnitTests.Bases
{
    public class DomainEventTests
    {
        [Fact]
        public void WhenDomainEventIsCreated_OcurredAtIsSetWithTheExactTimeAtWhichTheCreationHappens()
        {
            // Arrange
            var now = DateTime.UtcNow;
            TimeProvider.SetFixedTime(now);

            // Act
            var sut = new FakeDomainEvent();

            // Assert
            Assert.Equal(now, sut.OcurredAt);
        }
    }
}