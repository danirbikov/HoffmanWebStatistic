using System;
using System.Collections.Generic;
using HoffmanWebstatistic.Models.General;

namespace HoffmanWebstatistic.Models.Hoffman
{
    public partial class OsVersions
    {
        public OsVersions()
        {
            Stands = new HashSet<Stand>();
        }

        public int ID { get; set; }
        public string OSVersion { get; set; } = null!;

        public virtual ICollection<Stand> Stands { get; set; }
    }
}
