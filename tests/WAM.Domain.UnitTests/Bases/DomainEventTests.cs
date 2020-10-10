using Moq;
using System;
using System.Diagnostics.CodeAnalysis;
using WAM.Domain.Services;
using WAM.Domain.UnitTests.Fakes;
using Xunit;

namespace WAM.Domain.UnitTests.Bases
{
    [ExcludeFromCodeCoverage]
    public class DomainEventTests
    {
        private readonly Mock<ITimeService> _timeServiceMock;

        public DomainEventTests()
        {
            this._timeServiceMock = new Mock<ITimeService>();
        }

        [Fact]
        public void WhenDomainEventIsCreated_OcurredAtIsSetWithTheExactTimeAtWhichTheCreationHappens()
        {
            // Arrange
            var now = DateTime.UtcNow;
            _ = this._timeServiceMock.SetupGet(tsm => tsm.UtcNow).Returns(now);

            // Act
            var sut = new FakeDomainEvent(this._timeServiceMock.Object);

            // Assert
            Assert.Equal(now, sut.OcurredAt);
        }
    }
}