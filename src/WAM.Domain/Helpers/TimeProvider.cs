using System;

namespace WAM.Domain.Helpers
{
    /// <summary>
    /// Helper that allows DateTime abstraction.
    /// </summary>
    public static class TimeProvider
    {
        private static Func<DateTime> _dateTimeFunc = () => DateTime.UtcNow;

        /// <summary>
        /// Gets a <see cref="DateTime"/> object that is set to the current date and time on this computer,
        /// expressed as the Coordinated Universal Time (UTC).
        /// </summary>
        public static DateTime UtcNow => _dateTimeFunc();

        /// <summary>
        /// Sets the <see cref="TimeProvider"/> to return a fixed date and time,
        /// expressed as the Coordinated Universal Time (UTC).
        /// </summary>
        /// <param name="dateTime">The fixed time.</param>
        public static void SetFixedTime(DateTime dateTime) => _dateTimeFunc = () => dateTime;

        /// <summary>
        /// Sets the <see cref="TimeProvider"/> to return the current date and time on this computer,
        /// expressed as the Coordinated Universal Time (UTC).
        /// </summary>
        public static void SetRealTime() => _dateTimeFunc = () => DateTime.UtcNow;
    }
}