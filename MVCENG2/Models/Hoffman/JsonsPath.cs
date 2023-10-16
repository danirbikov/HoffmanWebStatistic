using HoffmanWebstatistic.Models.Hoffman;
using System;
using System.Collections.Generic;

namespace HoffmanWebstatistic
{
    public partial class JsonsPath
    {
        public int Id { get; set; }
        public int StandId { get; set; }
        public string CPath { get; set; } = null!;
        public string CLogin { get; set; } = null!;
        public string CPassword { get; set; } = null!;

        public virtual Stand Stand { get; set; } = null!;
    }
}
