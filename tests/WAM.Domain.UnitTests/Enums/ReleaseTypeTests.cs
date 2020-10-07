using WAM.Domain.Enums;
using Xunit;

namespace WAM.Domain.UnitTests.Enums
{
    public class ReleaseTypeTests
    {
        [Fact]
        public void ToString_Returns_ReleaseTypeName()
        {
            // Arrange
            var stableName = "Stable";
            var betaName = "Beta";
            var alphaName = "Alpha";

            // Act
            var stableReleaseTypeName = ReleaseType.Stable.ToString();
            var betaReleaseTypeName = ReleaseType.Beta.ToString();
            var alphaReleaseTypeName = ReleaseType.Alpha.ToString();

            // Assert
            Assert.Equal(stableName, stableReleaseTypeName);
            Assert.Equal(betaName, betaReleaseTypeName);
            Assert.Equal(alphaName, alphaReleaseTypeName);
        }

        [Fact]
        public void ToCurseforgeCode_Returns_CurseforgeReleaseTypeCode()
        {
            // Arrange
            var stableCurseforgeCode = 1;
            var betaCurseforgeCode = 2;
            var alphaCurseforgeCode = 3;

            // Act
            var stableReleaseTypeCurseforgeCode = ReleaseType.Stable.ToCurseforgeCode();
            var betaReleaseTypeCurseforgeCode = ReleaseType.Beta.ToCurseforgeCode();
            var alphaReleaseTypeCurseforgeCode = ReleaseType.Alpha.ToCurseforgeCode();

            // Assert
            Assert.Equal(stableCurseforgeCode, stableReleaseTypeCurseforgeCode);
            Assert.Equal(betaCurseforgeCode, betaReleaseTypeCurseforgeCode);
            Assert.Equal(alphaCurseforgeCode, alphaReleaseTypeCurseforgeCode);
        }

        [Fact]
        public void CanBeImplicitCastedToTheCorrespondingEnumValue()
        {
            // Arrange
            var stableEnum = ReleaseType.ReleaseTypeEnum.Stable;
            var betaEnum = ReleaseType.ReleaseTypeEnum.Beta;
            var alphaEnum = ReleaseType.ReleaseTypeEnum.Alpha;
            var stableReleaseType = ReleaseType.Stable;
            var betaReleaseType = ReleaseType.Beta;
            var alphaReleaseType = ReleaseType.Alpha;

            // Act
            var castedStableEnum = (ReleaseType.ReleaseTypeEnum)stableReleaseType;
            var castedBetaEnum = (ReleaseType.ReleaseTypeEnum)betaReleaseType;
            var castedAlphaEnum = (ReleaseType.ReleaseTypeEnum)alphaReleaseType;

            // Assert
            Assert.Equal(stableEnum, castedStableEnum);
            Assert.Equal(betaEnum, castedBetaEnum);
            Assert.Equal(alphaEnum, castedAlphaEnum);
        }

        [Fact]
        public void FromEnum_Returns_CorrespondingReleaseType()
        {
            // Arrange
            var stableReleaseType = ReleaseType.Stable;
            var betaReleaseType = ReleaseType.Beta;
            var alphaReleaseType = ReleaseType.Alpha;
            var stableEnum = ReleaseType.ReleaseTypeEnum.Stable;
            var betaEnum = ReleaseType.ReleaseTypeEnum.Beta;
            var alphaEnum = ReleaseType.ReleaseTypeEnum.Alpha;

            // Act
            var stable = ReleaseType.FromEnum(stableEnum);
            var beta = ReleaseType.FromEnum(betaEnum);
            var alpha = ReleaseType.FromEnum(alphaEnum);

            // Assert
            Assert.Equal(stableReleaseType, stable);
            Assert.Equal(betaReleaseType, beta);
            Assert.Equal(alphaReleaseType, alpha);
        }
    }
}