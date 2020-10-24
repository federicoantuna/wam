using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using WAM.Application.Common.Constants;
using WAM.Application.Common.Models;
using WAM.Domain.Bases;
using WAM.Domain.Logs;
using WAM.Domain.Services;
using WAM.Infrastructure.Services;
using WAM.Infrastructure.UnitTests.Fakes;
using Xunit;

namespace WAM.Infrastructure.UnitTests.Services
{
    [ExcludeFromCodeCoverage]
    public class DomainEventServiceTests
    {
        private readonly Mock<ITimeService> _timeServiceMock;
        private readonly Mock<ILogger<DomainEventService>> _loggerMock;
        private readonly Mock<IMediator> _mediatorMock;

        private readonly DomainEventService _sut;

        public DomainEventServiceTests()
        {
            this._timeServiceMock = new Mock<ITimeService>();
            this._loggerMock = new Mock<ILogger<DomainEventService>>();
            this._mediatorMock = new Mock<IMediator>();

            this._sut = new DomainEventService(this._timeServiceMock.Object,
                this._mediatorMock.Object,
                this._loggerMock.Object);
        }

        [Fact]
        public async Task Publish_LogsEventAndPublishesDomainEvent()
        {
            // Arrange
            var domainEvent = new FakeDomainEvent(this._timeServiceMock.Object);

            var domainEventName = typeof(FakeDomainEvent).Name;
            var formattedLogMessage = String.Format(LogMessage.PublishingDomainEventTemplate, domainEventName);

            // Act
            await this._sut.Publish(domainEvent);

            // Assert
            this._loggerMock.Verify(lm => lm.Log(LogLevel.Information, LogEvent.PublishingDomainEventEvent, domainEvent, null, It.Is<Func<DomainEvent, Exception, String>>(f => f(domainEvent, null) == formattedLogMessage)), Times.Once);
            this._mediatorMock.Verify(mm => mm.Publish(It.Is<INotification>(n => n.GetType() == typeof(DomainEventNotification<FakeDomainEvent>)), default), Times.Once);
        }

        [Fact]
        public void RaiseEvent_AddsEventToEntity()
        {
            // Arrange
            var entity = new FakeEntity();

            var now = DateTime.UtcNow;
            _ = this._timeServiceMock.SetupGet(tsm => tsm.UtcNow).Returns(now);

            // Act
            this._sut.RaiseEvent<FakeDomainEvent>(entity);

            // Assert
            _ = Assert.Single(entity.DomainEvents);
            _ = Assert.IsType<FakeDomainEvent>(entity.DomainEvents.Single());
            Assert.Collection(entity.DomainEvents,
                item => Assert.Equal(now, item.OcurredAt));
        }
    }
}