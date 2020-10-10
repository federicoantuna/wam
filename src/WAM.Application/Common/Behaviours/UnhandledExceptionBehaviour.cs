using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using WAM.Application.Common.Exceptions;
using WAM.Domain.Logs;

namespace WAM.Application.Common.Behaviours
{
    /// <inheritdoc/>
    /// <summary>
    /// Provides the Unhandled Exception Behaviour.
    /// </summary>
    /// <typeparam name="TRequest">Request type.</typeparam>
    /// <typeparam name="TResponse">Response type.</typeparam>
    public class UnhandledExceptionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<TRequest> _logger;

        /// <summary>
        /// Initializes the Unhandled Exception Behaviour with its dependencies.
        /// </summary>
        /// <param name="logger">The logger dependency.</param>
        public UnhandledExceptionBehaviour(ILogger<TRequest> logger)
        {
            this._logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            try
            {
                return await next();
            }
            catch (Exception ex)
            {
                var requestName = typeof(TRequest).Name;

                String formatter(TRequest r, Exception e) => String.Format(ExceptionMessage.UnhandledExceptionLogTemplate, requestName, r);
                this._logger.Log(LogLevel.Error, LogEvent.UnhandledExceptionEvent, request, ex, formatter);

                throw;
            }
        }
    }
}