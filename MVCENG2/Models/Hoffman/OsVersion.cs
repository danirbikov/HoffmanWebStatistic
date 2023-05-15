using System;
using System.Collections.Generic;
using MVCENG2.Models.General;

namespace MVCENG2.Models.Hoffman
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
