using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.IO.Compression;
using System.Linq;
using WAM.Infrastructure.Abstractions;
using Xunit;

namespace WAM.Infrastructure.UnitTests.Abstractions
{
    [ExcludeFromCodeCoverage]
    public class SystemZipArchiveTests
    {
        [Fact]
        public void GetRootEntries_ReturnsDistinctRootEntries()
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

            var sut = new SystemZipArchive(memoryStream, ZipArchiveMode.Read);

            var expectedEntries = new String[]
            {
                rootEntryA,
                rootEntryB,
                rootEntryC
            };

            // Act
            var result = sut.GetRootEntries().OrderBy(re => re);

            // Assert
            Assert.Collection(result,
                item => Assert.Equal(expectedEntries[0], result.ElementAt(0)),
                item => Assert.Equal(expectedEntries[1], result.ElementAt(1)),
                item => Assert.Equal(expectedEntries[2], result.ElementAt(2)));
            
            memoryStream.Dispose();
        }
    }
}