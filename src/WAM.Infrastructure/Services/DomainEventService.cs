using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using WAM.Application.Common.Constants;
using WAM.Application.Common.Interfaces;
using WAM.Application.Common.Models;
using WAM.Domain.Bases;
using WAM.Domain.Logs;
using WAM.Domain.Services;

namespace WAM.Infrastructure.Services
{
    /// <inheritdoc/>
    /// <summary>
    /// Service to manage Domain Events.
    /// </summary>
    public class DomainEventService : IDomainEventService
    {
        private readonly ITimeService _timeService;
        private readonly IMediator _mediator;
        private readonly ILogger<DomainEventService> _logger;

        /// <summary>
        /// Initializes the Domain Event Service.
        /// </summary>
        /// <param name="timeService">The Time Service dependency.</param>
        /// <param name="logger">The Logger dependency.</param>
        /// <param name="mediator">The Mediator dependency.</param>
        public DomainEventService(ITimeService timeService,
            IMediator mediator,
            ILogger<DomainEventService> logger)
        {
            this._timeService = timeService;
            this._mediator = mediator;
            this._logger = logger;
        }

        public async Task Publish(DomainEvent domainEvent)
        {
            static String formatter(DomainEvent de, Exception e) => String.Format(LogMessage.PublishingDomainEventTemplate, de.GetType().Name);
            this._logger.Log(LogLevel.Information, LogEvent.PublishingDomainEventEvent, domainEvent, null, formatter);

            var test = this.GetNotificationCorrespondingToDomainEvent(domainEvent);

            await this._mediator.Publish(test);
        }

        public void RaiseEvent<TEvent>(Entity entity)
            where TEvent : DomainEvent
        {
            var @event = (DomainEvent)Activator.CreateInstance(typeof(TEvent), this._timeService);

            entity.AddDomainEvent(@event);
        }

        private INotification GetNotificationCorrespondingToDomainEvent(DomainEvent domainEvent) =>
            (INotification)Activator.CreateInstance(typeof(DomainEventNotification<>).MakeGenericType(domainEvent.GetType()), domainEvent);
    }
}