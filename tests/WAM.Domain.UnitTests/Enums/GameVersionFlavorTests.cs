using System.Diagnostics.CodeAnalysis;
using WAM.Domain.Enums;
using Xunit;

namespace WAM.Domain.UnitTests.Enums
{
    [ExcludeFromCodeCoverage]
    public class GameVersionFlavorTests
    {
        [Fact]
        public void ToString_Returns_GameVersionFlavorName()
        {
            // Arrange
            var retailName = "Retail";
            var classicName = "Classic";

            // Act
            var retailGameVersionFlavorName = GameVersionFlavor.Retail.ToString();
            var classicGameVersionFlavorName = GameVersionFlavor.Classic.ToString();

            // Assert
            Assert.Equal(retailName, retailGameVersionFlavorName);
            Assert.Equal(classicName, classicGameVersionFlavorName);
        }

        [Fact]
        public void ToCurseforgeCode_Returns_CurseforgeGameVersionFlavorCode()
        {
            // Arrange
            var retailCurseforgeCode = "wow_retail";
            var classicCurseforgeCode = "wow_classic";

            // Act
            var retailGameVersionFlavorCurseforgeCode = GameVersionFlavor.Retail.ToCurseforgeCode();
            var classicGameVersionFlavorCurseforgeCode = GameVersionFlavor.Classic.ToCurseforgeCode();

            // Assert
            Assert.Equal(retailCurseforgeCode, retailGameVersionFlavorCurseforgeCode);
            Assert.Equal(classicCurseforgeCode, classicGameVersionFlavorCurseforgeCode);
        }

        [Fact]
        public void CanBeImplicitCastedToTheCorrespondingEnumValue()
        {
            // Arrange
            var retailGameVersionFlavor = GameVersionFlavor.Retail;
            var classicGameVersionFlavor = GameVersionFlavor.Classic;
            
            var retailEnum = GameVersionFlavor.GameVersionFlavorEnum.Retail;
            var classicEnum = GameVersionFlavor.GameVersionFlavorEnum.Classic;

            // Act
            var castedRetailEnum = (GameVersionFlavor.GameVersionFlavorEnum)retailGameVersionFlavor;
            var castedClassicEnum = (GameVersionFlavor.GameVersionFlavorEnum)classicGameVersionFlavor;

            // Assert
            Assert.Equal(retailEnum, castedRetailEnum);
            Assert.Equal(classicEnum, castedClassicEnum);
        }

        [Fact]
        public void FromCode_Returns_CorrespondingGameVersionFlavor()
        {
            // Arrange
            var retailCode = "wow_retail";
            var classicCode = "wow_classic";

            var retailGameVersionFlavor = GameVersionFlavor.Retail;
            var classicGameVersionFlavor = GameVersionFlavor.Classic;

            // Act
            var retail = GameVersionFlavor.FromCode(retailCode);
            var classic = GameVersionFlavor.FromCode(classicCode);

            // Assert
            Assert.Equal(retailGameVersionFlavor, retail);
            Assert.Equal(classicGameVersionFlavor, classic);
        }

        [Fact]
        public void FromEnum_Returns_CorrespondingGameVersionFlavor()
        {
            // Arrange
            var retailEnum = GameVersionFlavor.GameVersionFlavorEnum.Retail;
            var classicEnum = GameVersionFlavor.GameVersionFlavorEnum.Classic;

            var retailGameVersionFlavor = GameVersionFlavor.Retail;
            var classicGameVersionFlavor = GameVersionFlavor.Classic;

            // Act
            var retail = GameVersionFlavor.FromEnum(retailEnum);
            var classic = GameVersionFlavor.FromEnum(classicEnum);

            // Assert
            Assert.Equal(retailGameVersionFlavor, retail);
            Assert.Equal(classicGameVersionFlavor, classic);
        }
    }
}