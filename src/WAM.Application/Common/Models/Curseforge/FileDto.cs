using System;

namespace WAM.Application.Common.Models.Curseforge
{
    public class FileDto
    {
        public String DownloadUrl { get; set; }

        public String Name { get; set; }

        public String Version { get; set; }

        public String GameVersionFlavor { get; set; }

        public Int32 ReleaseType { get; set; }
    }
}