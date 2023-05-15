﻿using System;
using System.Collections.Generic;
using MVCENG2.Models.General;

namespace MVCENG2.Models.Hoffman
{
    public partial class Mes2supTelegramsStand
    {
        public long Id { get; set; }
        public long TgId { get; set; }
        public int StandId { get; set; }
        public DateTime Transfered { get; set; }

        public virtual Stand Stand { get; set; } = null!;
        public virtual Mes2supTelegram Tg { get; set; } = null!;
    }
}
