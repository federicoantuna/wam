using Microsoft.Extensions.Logging;
using System;

namespace WAM.Domain.Logs
{
    public static class LogEvent
    {
        #region GenericCodes
        private const Int32 ExceptionCode = 100;
        #endregion

        #region SpecificCodes
        private const Int32 UnhandledExceptionCode = 0;
        #endregion

        #region Names
        private const String UnhandledException = nameof(UnhandledException);
        #endregion

        /// <summary>
        /// Represents an unhandled exception.
        /// </summary>
        public static EventId UnhandledExceptionEvent => new EventId(ExceptionCode + UnhandledExceptionCode, UnhandledException);
    }
}