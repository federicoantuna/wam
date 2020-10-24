using Microsoft.Extensions.Logging;
using System;

namespace WAM.Domain.Logs
{
    public static class LogEvent
    {
        #region GenericCodes
        private const Int32 ExceptionCode = 100;

        private const Int32 DomainEventCode = 200;
        #endregion

        #region SpecificCodes
        private const Int32 UnhandledExceptionCode = 0;

        private const Int32 PublishingDomainEventCode = 0;
        #endregion

        #region Names
        private const String UnhandledException = nameof(UnhandledException);

        private const String DomainEvent = nameof(DomainEvent);
        #endregion

        /// <summary>
        /// Represents an unhandled exception.
        /// </summary>
        public static EventId UnhandledExceptionEvent => new EventId(ExceptionCode + UnhandledExceptionCode, UnhandledException);

        /// <summary>
        /// Represents a domain event being published.
        /// </summary>
        public static EventId PublishingDomainEventEvent => new EventId(DomainEventCode + PublishingDomainEventCode, DomainEvent);
    }
}