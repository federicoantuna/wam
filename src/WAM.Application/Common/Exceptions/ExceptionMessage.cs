using System;

namespace WAM.Application.Common.Exceptions
{
    /// <summary>
    /// Contains message templates related to exceptions.
    /// </summary>
    public static class ExceptionMessage
    {
        /// <summary>
        /// Unhandled exception for Request {requestName}, {request}.
        /// </summary>
        public static String UnhandledExceptionLogTemplate => "Unhandled exception for Request {0}, {1}.";
    }
}