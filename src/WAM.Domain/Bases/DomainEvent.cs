using System;
using WAM.Domain.Services;

namespace WAM.Domain.Bases
{
    /// <summary>
    /// Base for domain events.
    /// </summary>
    public abstract class DomainEvent
    {
        /// <summary>
        /// Initializes the <see cref="DomainEvent"/>.
        /// </summary>
        /// <param name="timeService">The Time Service dependency.</param>
        public DomainEvent(ITimeService timeService)
        {
            this.OcurredAt = timeService.UtcNow;
        }

        /// <summary>
        /// Exact time when the event ocurred.
        /// </summary>
        public DateTimeOffset OcurredAt { get; protected set; }
    }
}