using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.IO.Compression;
using WAM.Infrastructure.Abstractions;
using WAM.Infrastructure.Factories;
using Xunit;

namespace WAM.Infrastructure.UnitTests.Factories
{
    [ExcludeFromCodeCoverage]
    public class SystemZipArchiveFactoryTests
    {
        private readonly SystemZipArchiveFactory _sut;

        public SystemZipArchiveFactoryTests()
        {
            this._sut = new SystemZipArchiveFactory();
        }

        [Fact]
        public void Create_ReturnsSystemZipArchive()
        {
            // Arrange
            var rootEntryA = "test_dir_a";
            var rootEntryB = "test_dir_b";
            var rootEntryC = "test_dir_c";
            var rootEntryBBA = $"{rootEntryB}/test_dir_ba";
            var rootEntryCCA = $"{rootEntryC}/test_dir_ca";
            var rootEntryCCB = $"{rootEntryC}/test_dir_cb";
            var memoryStream = new MemoryStream();
            using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, leaveOpen: true))
            {
                _ = archive.CreateEntry(rootEntryA);
                _ = archive.CreateEntry(rootEntryB);
                _ = archive.CreateEntry(rootEntryBBA);
                _ = archive.CreateEntry(rootEntryCCA);
                _ = archive.CreateEntry(rootEntryCCB);
            }

            // Act
            var result = this._sut.Create(memoryStream, ZipArchiveMode.Read);

            // Assert
            Assert.NotNull(result);
            _ = Assert.IsType<SystemZipArchive>(result);
        }
    }
}