using System;
using System.Collections.Generic;
using PingerAPI.Models.General;

namespace PingerAPI.Models.Hoffman
{
    public partial class Sup2mesPath
    {
        public int Id { get; set; }
        public int StandId { get; set; }
        public string CPath { get; set; } = null!;
        public string CLogin { get; set; } = null!;
        public string CPassword { get; set; } = null!;

        public virtual Stand Stand { get; set; } = null!;
    }
}
