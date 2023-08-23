using System;
using System.Collections.Generic;
using HoffmanWebstatistic.Models.General;

namespace HoffmanWebstatistic.Models.Hoffman
{
    public partial class OperatorsStand
    {
        public int Id { get; set; }
        public int OperatorId { get; set; }
        public int StandId { get; set; }

        public virtual Operator Operator { get; set; } = null!;
        public virtual Stand Stand { get; set; } = null!;
    }
}
