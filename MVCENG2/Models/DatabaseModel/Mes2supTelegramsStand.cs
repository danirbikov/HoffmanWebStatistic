﻿using System;
using System.Collections.Generic;

namespace HoffmanWebstatistic.Models.Hoffman
{
    public partial class Mes2supTelegramsStand
    {
        public long Id { get; set; }
        public long TgId { get; set; }
        public int StandId { get; set; }
        public DateTime Transfered { get; set; }
        public string? TransferStatus { get; set; }


        public virtual Stand Stand { get; set; } = null!;
        public virtual Mes2supTelegram Tg { get; set; } = null!;
    }
}
