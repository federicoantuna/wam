using System;
using System.Diagnostics.CodeAnalysis;
using WAM.Application.Common.Models;
using Xunit;

namespace WAM.Application.UnitTests.Common.Models
{
    [ExcludeFromCodeCoverage]
    public class ResultTests
    {
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void WhenResultIsCreated_WithIsSuccessfulParameter_SetsIsSuccessfulToCorrespondingValue(Boolean isSuccessful)
        {
            // Arrange
            // Act
            var sut = new Result(isSuccessful);

            // Assert
            Assert.Equal(isSuccessful, sut.IsSuccessful);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void WhenResultIsCreated_WithIsSuccessfulAndMessagesParameters_SetsIsSuccessfulAndMessagesToCorrespondingValues(Boolean isSuccessful)
        {
            // Arrange
            var messageA = "Test Message A";
            var messageB = "Test Message B";
            var messageC = "Test Message C";
            var messages = new String[]
            {
                messageA,
                messageB,
                messageC
            };

            // Act
            var sut = new Result(isSuccessful, messages);

            // Assert
            Assert.Equal(isSuccessful, sut.IsSuccessful);
            Assert.Collection(sut.Messages,
                item => Assert.Equal(messageA, item),
                item => Assert.Equal(messageB, item),
                item => Assert.Equal(messageC, item));
        }

        [Fact]
        public void AddMessage_AddsAMessageToResult()
        {
            // Arrange
            var messageA = "Test Message A";

            var isSuccessful = true;

            var sut = new Result(isSuccessful);

            // Act
            sut.AddMessage(messageA);

            // Assert
            _ = Assert.Single(sut.Messages);
            Assert.Collection(sut.Messages,
                item => Assert.Equal(messageA, item));
        }

        [Fact]
        public void AddMessages_AddsAllMessagesToResult()
        {
            // Arrange
            var messageA = "Test Message A";
            var messageB = "Test Message B";
            var messageC = "Test Message C";
            var messages = new String[]
            {
                messageA,
                messageB,
                messageC
            };

            var isSuccessful = true;

            var sut = new Result(isSuccessful);

            // Act
            sut.AddMessages(messages);

            // Assert
            Assert.NotEmpty(sut.Messages);
            Assert.Collection(sut.Messages,
                item => Assert.Equal(messageA, item),
                item => Assert.Equal(messageB, item),
                item => Assert.Equal(messageC, item));
        }
    }
}