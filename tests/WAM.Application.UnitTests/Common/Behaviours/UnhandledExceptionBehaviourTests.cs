using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using WAM.Application.Common.Behaviours;
using WAM.Application.Common.Exceptions;
using WAM.Application.UnitTests.Fakes;
using WAM.Domain.Logs;
using Xunit;

namespace WAM.Application.UnitTests.Common.Behaviours
{
    [ExcludeFromCodeCoverage]
    public class UnhandledExceptionBehaviourTests
    {
        private readonly Mock<ILogger<FakeRequest>> _loggerMock;

        private readonly UnhandledExceptionBehaviour<FakeRequest, String> _sut;

        public UnhandledExceptionBehaviourTests()
        {
            this._loggerMock = new Mock<ILogger<FakeRequest>>();

            this._sut = new UnhandledExceptionBehaviour<FakeRequest, String>(this._loggerMock.Object);
        }

        [Fact]
        public async Task Handle_WhenRequestHandlerDelegateIsValid_ReturnsHandlerResult()
        {
            // Arrange
            var handlerResponse = "Test Handler";
            var request = new FakeRequest();
            var cancellationToken = default(CancellationToken);
            async Task<String> handler() => await Task.FromResult(handlerResponse);

            // Act
            var result = await this._sut.Handle(request, cancellationToken, handler);

            // Assert
            Assert.Equal(handlerResponse, result);
        }

        [Fact]
        public async Task Handle_WhenRequestHandlerDelegateIsNotValid_LogsExceptionAndRethrows()
        {
            // Arrange
            var exceptionMessage = "Test Exception";
            var exception = new Exception(exceptionMessage);
            var request = new FakeRequest();
            var cancellationToken = default(CancellationToken);
            async Task<String> handler() => await Task.FromException<String>(exception);
            
            var requestName = typeof(FakeRequest).Name;
            var formattedLogMessage = String.Format(ExceptionMessage.UnhandledExceptionLogTemplate, requestName, request);

            // Act
            // Assert
            var result = await Assert.ThrowsAsync<Exception>(() => this._sut.Handle(request, cancellationToken, handler));
            this._loggerMock.Verify(lm => lm.Log(LogLevel.Error, LogEvent.UnhandledExceptionEvent, request, exception, It.Is<Func<FakeRequest, Exception, String>>(f => f(request, exception) == formattedLogMessage)), Times.Once);
            Assert.Equal(exceptionMessage, result.Message);
        }
    }
}