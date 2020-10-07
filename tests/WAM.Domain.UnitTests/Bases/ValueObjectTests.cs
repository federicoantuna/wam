using WAM.Domain.UnitTests.Fakes;
using Xunit;

namespace WAM.Domain.UnitTests.Bases
{
    public class ValueObjectTests
    {
        [Fact]
        public void GetHashCode_ForEquivalentValueObjects_ReturnsSameHashCodes()
        {
            // Arrange
            var differentiator = 1;
            var sutA = new FakeValueObject
            {
                Differentiator = differentiator
            };
            var sutB = new FakeValueObject
            {
                Differentiator = differentiator
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
                Differentiator = differentiatorA
            };
            var sutB = new FakeValueObject
            {
                Differentiator = differentiatorB
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
                Differentiator = differentiator
            };
            var sutB = new FakeValueObject
            {
                Differentiator = differentiator
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
            var sutA = new FakeValueObject
            {
                Differentiator = differentiatorA
            };
            var differentiatorB = 2;
            var sutB = new FakeValueObject
            {
                Differentiator = differentiatorB
            };

            // Act
            var AEqualsB = sutA.Equals(sutB);
            var BEqualsA = sutB.Equals(sutA);

            // Assert
            Assert.False(AEqualsB);
            Assert.False(BEqualsA);
        }

        [Fact]
        public void EqualOperator_ForEquivalentValueObjects_ReturnsTrue()
        {
            // Arrange
            var differentiator = 1;
            var sutA = new FakeValueObject
            {
                Differentiator = differentiator
            };
            var sutB = new FakeValueObject
            {
                Differentiator = differentiator
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
            var sutA = new FakeValueObject
            {
                Differentiator = differentiatorA
            };
            var differentiatorB = 2;
            var sutB = new FakeValueObject
            {
                Differentiator = differentiatorB
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
            var sutA = new FakeValueObject
            {
                Differentiator = differentiatorA
            };
            var differentiatorB = 2;
            var sutB = new FakeValueObject
            {
                Differentiator = differentiatorB
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
                Differentiator = differentiator
            };
            var sutB = new FakeValueObject
            {
                Differentiator = differentiator
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