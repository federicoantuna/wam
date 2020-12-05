using System;
using System.Collections.Generic;
using WAM.Domain.Entities;
using WAM.Domain.Enums;

namespace WAM.Application.Common.Models
{
    public class AddonDto
    {
        public String Name { get; set; }

        public String Version { get; set; }

        public GameVersionFlavor GameVersionFlavor { get; set; }

        public ReleaseType ReleaseType { get; set; }

        public Package Package { get; set; }

        public IEnumerable<String> ModuleNames { get; set; }
    }
}