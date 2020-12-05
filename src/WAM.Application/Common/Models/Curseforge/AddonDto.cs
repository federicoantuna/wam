using System;

namespace WAM.Application.Common.Models.Curseforge
{
    public class AddonDto
    {
        public Int32 Id { get; set; }

        public String Name { get; set; }

        public String Slug { get; set; }

        public FileDto[] LatestFiles { get; set; }
    }
}