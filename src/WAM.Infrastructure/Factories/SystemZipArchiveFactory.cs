using System.IO;
using System.IO.Compression;
using WAM.Application.Common.Interfaces;
using WAM.Application.Common.Interfaces.Abstractions;
using WAM.Infrastructure.Abstractions;

namespace WAM.Infrastructure.Factories
{
    /// <inheritdoc/>
    /// <summary>
    /// Factory for <see cref="IZipArchive"/> type.
    /// </summary>
    public class SystemZipArchiveFactory : ISystemZipArchiveFactory
    {
        public IZipArchive Create(Stream stream, ZipArchiveMode zipArchiveMode) => new SystemZipArchive(stream, zipArchiveMode);
    }
}