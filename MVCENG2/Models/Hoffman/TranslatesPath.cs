using System;
using System.Collections.Generic;
using MVCENG2.Models.General;

namespace MVCENG2.Models.Hoffman
{
    public partial class TranslatesPath
    {
        public int Id { get; set; }
        public int StandId { get; set; }
        public string CPath { get; set; } = null!;
        public string CLogin { get; set; } = null!;
        public string CPassword { get; set; } = null!;

        public virtual Stand Stand { get; set; } = null!;
    }
}
