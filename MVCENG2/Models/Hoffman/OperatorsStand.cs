using System;
using System.Collections.Generic;
using MVCENG2.Models.General;

namespace MVCENG2.Models.Hoffman
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
