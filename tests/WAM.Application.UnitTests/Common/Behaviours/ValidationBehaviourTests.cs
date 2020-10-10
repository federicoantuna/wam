using FluentValidation;
using FluentValidation.Results;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WAM.Application.Common.Behaviours;
using WAM.Application.UnitTests.Fakes;
using Xunit;

namespace WAM.Application.UnitTests.Common.Behaviours
{
    [ExcludeFromCodeCoverage]
    public class ValidationBehaviourTests
    {
        private readonly Mock<IValidator<FakeRequest>> _idValidatorMock;
        private readonly Mock<IValidator<FakeRequest>> _textValidatorMock;

        private readonly IEnumerable<IValidator<FakeRequest>> _validators;

        public ValidationBehaviourTests()
        {
            this._idValidatorMock = new Mock<IValidator<FakeRequest>>();
            this._textValidatorMock = new Mock<IValidator<FakeRequest>>();

            this._validators = new List<IValidator<FakeRequest>>
            {
                this._idValidatorMock.Object,
                this._textValidatorMock.Object
            };
        }

        [Fact]
        public async Task Handle_WhenThereAreNoValidators_ReturnsHandlerResult()
        {
            // Arrange
            var handlerResponse = "Test Handler";
            var request = new FakeRequest();
            var cancellationToken = default(CancellationToken);
            async Task<String> handler() => await Task.FromResult(handlerResponse);
            
            var sut = new ValidationBehaviour<FakeRequest, String>(Enumerable.Empty<IValidator<FakeRequest>>());

            // Act
            var result = await sut.Handle(request, cancellationToken, handler);

            // Assert
            Assert.Equal(handlerResponse, result);
        }

        [Fact]
        public async Task Handle_WhenThereAreValidatorsAndNoFailures_ReturnsHandlerResult()
        {
            // Arrange
            var handlerResponse = "Test Handler";
            var request = new FakeRequest
            {
                Id = 0,
                SomeText = "Test Text"
            };
            var cancellationToken = default(CancellationToken);
            async Task<String> handler() => await Task.FromResult(handlerResponse);

            _ = this._idValidatorMock.Setup(v => v.ValidateAsync(It.Is<ValidationContext<FakeRequest>>(c => c.InstanceToValidate.Id == request.Id && c.InstanceToValidate.SomeText == request.SomeText), cancellationToken)).ReturnsAsync(new ValidationResult());
            _ = this._textValidatorMock.Setup(v => v.ValidateAsync(It.Is<ValidationContext<FakeRequest>>(c => c.InstanceToValidate.Id == request.Id && c.InstanceToValidate.SomeText == request.SomeText), cancellationToken)).ReturnsAsync(new ValidationResult());

            var sut = new ValidationBehaviour<FakeRequest, String>(this._validators);

            // Act
            var result = await sut.Handle(request, cancellationToken, handler);

            // Assert
            Assert.Equal(handlerResponse, result);
        }

        [Fact]
        public async Task Handle_WhenThereAreValidatorsAndFailures_TrhowsValidationException()
        {
            // Arrange
            var handlerResponse = "Test Handler";
            var request = new FakeRequest
            {
                Id = 0,
                SomeText = "Test Text"
            };
            var cancellationToken = default(CancellationToken);
            async Task<String> handler() => await Task.FromResult(handlerResponse);

            var failureMessage = "Test Failure";
            var failures = new List<ValidationFailure>
            {
                new ValidationFailure(nameof(FakeRequest.SomeText), failureMessage)
            };
            _ = this._idValidatorMock.Setup(v => v.ValidateAsync(It.Is<ValidationContext<FakeRequest>>(c => c.InstanceToValidate.Id == request.Id && c.InstanceToValidate.SomeText == request.SomeText), cancellationToken)).ReturnsAsync(new ValidationResult());
            _ = this._textValidatorMock.Setup(v => v.ValidateAsync(It.Is<ValidationContext<FakeRequest>>(c => c.InstanceToValidate.Id == request.Id && c.InstanceToValidate.SomeText == request.SomeText), cancellationToken)).ReturnsAsync(new ValidationResult(failures));

            var sut = new ValidationBehaviour<FakeRequest, String>(this._validators);

            // Act
            // Assert
            var result = await Assert.ThrowsAsync<ValidationException>(() => sut.Handle(request, cancellationToken, handler));
            Assert.Contains(failureMessage, result.Message);
        }
    }
}