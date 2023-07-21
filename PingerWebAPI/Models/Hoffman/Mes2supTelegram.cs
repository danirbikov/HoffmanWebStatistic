using System;
using System.Collections.Generic;

namespace PingerAPI.Models.Hoffman
{
    public partial class Mes2supTelegram
    {
        public Mes2supTelegram()
        {
            Mes2supTelegramsStands = new HashSet<Mes2supTelegramsStand>();
        }

        public long Id { get; set; }
        public string Vin { get; set; } = null!;
        public string Ordernum { get; set; } = null!;
        public string TgFilename { get; set; } = null!;
        public DateTime Created { get; set; }
        public string TgContent { get; set; } = null!;

        public virtual ICollection<Mes2supTelegramsStand> Mes2supTelegramsStands { get; set; }
    }
}
