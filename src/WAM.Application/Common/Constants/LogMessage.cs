using System;

namespace WAM.Application.Common.Constants
{
    /// <summary>
    /// Contains message templates.
    /// </summary>
    public static class LogMessage
    {
        /// <summary>
        /// Unhandled exception message for Request {requestName}, {request}.
        /// </summary>
        public const String UnhandledExceptionLogTemplate = "Unhandled exception for Request {0}, {1}.";

        /// <summary>
        /// Publishing domain event. Event - {domainEventName}.
        /// </summary>
        public const String PublishingDomainEventTemplate = "Publishing domain event. Event - {0}.";
    }
}