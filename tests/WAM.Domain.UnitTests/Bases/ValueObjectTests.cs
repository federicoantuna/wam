using System;
using System.Diagnostics.CodeAnalysis;
using WAM.Domain.UnitTests.Fakes;
using Xunit;

namespace WAM.Domain.UnitTests.Bases
{
    [ExcludeFromCodeCoverage]
    public class ValueObjectTests
    {
        [Fact]
        public void GetHashCode_WhenPropertiesAreNull_ReturnsZero()
        {
            // Arrange
            var sut = new FakeValueObject
            {
                TestNullableInt = null
            };

            // Act
            var result = sut.GetHashCode();

            // Assert
            Assert.Equal(0, result);
        }

        [Fact]
        public void GetHashCode_WhenPropertiesAreNotNull_ReturnsXOROfEachPropertyHashCode()
        {
            // Arrange
            var testNumber = new Random().Next();
            var testString = "Test String";

            var sut = new FakeValueObject
            {
                TestNullableInt = testNumber,
                TestString = testString
            };

            var testNumberHashCode = testNumber.GetHashCode();
            var testStringHashCode = testString.GetHashCode();
            var expectedHashCode = testNumberHashCode ^ testStringHashCode;

            // Act
            var result = sut.GetHashCode();

            // Assert
            Assert.Equal(expectedHashCode, result);
        }

        [Fact]
        public void GetHashCode_ForEquivalentValueObjects_ReturnsSameHashCodes()
        {
            // Arrange
            var differentiator = 1;

            var sutA = new FakeValueObject
            {
                TestNullableInt = differentiator
            };
            var sutB = new FakeValueObject
            {
                TestNullableInt = differentiator
            };

            // Act
            var hashCodeA = sutA.GetHashCode();
            var hashCodeB = sutB.GetHashCode();

            // Assert
            Assert.Equal(hashCodeA, hashCodeB);
        }

        [Fact]
        public void GetHashCode_ForDifferentValueObjects_ReturnsDifferentHashCodes()
        {
            // Arrange
            var differentiatorA = 1;
            var differentiatorB = 2;

            var sutA = new FakeValueObject
            {
                TestNullableInt = differentiatorA
            };
            var sutB = new FakeValueObject
            {
                TestNullableInt = differentiatorB
            };

            // Act
            var hashCodeA = sutA.GetHashCode();
            var hashCodeB = sutB.GetHashCode();

            // Assert
            Assert.NotEqual(hashCodeA, hashCodeB);
        }

        [Fact]
        public void Equals_ForEquivalentValueObjects_ReturnsTrue()
        {
            // Arrange
            var differentiator = 1;

            var sutA = new FakeValueObject
            {
                TestNullableInt = differentiator
            };
            var sutB = new FakeValueObject
            {
                TestNullableInt = differentiator
            };

            // Act
            var AEqualsB = sutA.Equals(sutB);
            var BEqualsA = sutB.Equals(sutA);

            // Assert
            Assert.True(AEqualsB);
            Assert.True(BEqualsA);
        }

        [Fact]
        public void Equals_ForDifferentValueObjects_ReturnsFalse()
        {
            // Arrange
            var differentiatorA = 1;
            var differentiatorB = 2;

            var sutA = new FakeValueObject
            {
                TestNullableInt = differentiatorA
            };
            var sutB = new FakeValueObject
            {
                TestNullableInt = differentiatorB
            };

            // Act
            var AEqualsB = sutA.Equals(sutB);
            var BEqualsA = sutB.Equals(sutA);

            // Assert
            Assert.False(AEqualsB);
            Assert.False(BEqualsA);
        }

        [Fact]
        public void Equals_WhenComparingWithNull_ReturnsFalse()
        {
            // Arrange
            var sut = new FakeValueObject();

            // Act
            var result = sut.Equals(null);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Equals_WhenComparingWithDifferentType_ReturnsFalse()
        {
            // Arrange
            var differentType = new Object();

            var sut = new FakeValueObject();

            // Act
            var result = sut.Equals(differentType);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void EqualOperator_ForEquivalentValueObjects_ReturnsTrue()
        {
            // Arrange
            var differentiator = 1;

            var sutA = new FakeValueObject
            {
                TestNullableInt = differentiator
            };
            var sutB = new FakeValueObject
            {
                TestNullableInt = differentiator
            };

            // Act
            var AEqualsB = sutA == sutB;
            var BEqualsA = sutB == sutA;

            // Assert
            Assert.True(AEqualsB);
            Assert.True(BEqualsA);
        }

        [Fact]
        public void EqualOperator_ForDifferentValueObjects_ReturnsFalse()
        {
            // Arrange
            var differentiatorA = 1;
            var differentiatorB = 2;

            var sutA = new FakeValueObject
            {
                TestNullableInt = differentiatorA
            };
            var sutB = new FakeValueObject
            {
                TestNullableInt = differentiatorB
            };

            // Act
            var AEqualsB = sutA == sutB;
            var BEqualsA = sutB == sutA;

            // Assert
            Assert.False(AEqualsB);
            Assert.False(BEqualsA);
        }

        [Fact]
        public void NotEqualOperator_ForDifferentValueObjects_ReturnsTrue()
        {
            // Arrange
            var differentiatorA = 1;
            var differentiatorB = 2;

            var sutA = new FakeValueObject
            {
                TestNullableInt = differentiatorA
            };
            var sutB = new FakeValueObject
            {
                TestNullableInt = differentiatorB
            };

            // Act
            var AEqualsB = sutA != sutB;
            var BEqualsA = sutB != sutA;

            // Assert
            Assert.True(AEqualsB);
            Assert.True(BEqualsA);
        }

        [Fact]
        public void NotEqualOperator_ForEquivalentValueObjects_ReturnsFalse()
        {
            // Arrange
            var differentiator = 1;

            var sutA = new FakeValueObject
            {
                TestNullableInt = differentiator
            };
            var sutB = new FakeValueObject
            {
                TestNullableInt = differentiator
            };

            // Act
            var AEqualsB = sutA != sutB;
            var BEqualsA = sutB != sutA;

            // Assert
            Assert.False(AEqualsB);
            Assert.False(BEqualsA);
        }
    }
}