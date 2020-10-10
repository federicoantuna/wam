using System;
using WAM.Domain.Services;

namespace WAM.Infrastructure.Services
{
    /// <inheritdoc/>
    /// <summary>
    /// Service that allows DateTime abstraction.
    /// </summary>
    public class TimeService : ITimeService
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}