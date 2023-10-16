using System;
using System.Collections.Generic;

namespace HoffmanWebstatistic
{
    public partial class DtcContent
    {
        public int Id { get; set; }
        public string Fname { get; set; } = null!;
        public string Fdata { get; set; } = null!;
        public DateTime Created { get; set; }
    }
}
