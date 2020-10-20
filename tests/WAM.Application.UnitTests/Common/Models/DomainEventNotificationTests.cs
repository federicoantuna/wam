using Moq;
using System.Diagnostics.CodeAnalysis;
using WAM.Application.Common.Models;
using WAM.Application.UnitTests.Fakes;
using WAM.Domain.Services;
using Xunit;

namespace WAM.Application.UnitTests.Common.Models
{
    [ExcludeFromCodeCoverage]
    public class DomainEventNotificationTests
    {
        private readonly Mock<ITimeService> _timeServiceMock;

        public DomainEventNotificationTests()
        {
            this._timeServiceMock = new Mock<ITimeService>();
        }

        [Fact]
        public void WhenDomainEventNotificationIsCreated_DomainEventIsSet()
        {
            // Arrange
            var fakeDomainEvent = new FakeDomainEvent(this._timeServiceMock.Object);

            // Act
            var sut = new DomainEventNotification<FakeDomainEvent>(fakeDomainEvent);

            // Result
            Assert.Equal(fakeDomainEvent, sut.DomainEvent);
        }
    }
}