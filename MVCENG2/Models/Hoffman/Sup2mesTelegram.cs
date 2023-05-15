using System;
using System.Collections.Generic;
using MVCENG2.Models.General;

namespace MVCENG2.Models.Hoffman
{
    public partial class Sup2mesTelegram
    {
        public long Id { get; set; }
        public string Vin { get; set; } = null!;
        public string Ordernum { get; set; } = null!;
        public string TgFilename { get; set; } = null!;
        public int StandId { get; set; }
        public DateTime Created { get; set; }
        public string TgContent { get; set; } = null!;

        public virtual Stand Stand { get; set; } = null!;
    }
}
