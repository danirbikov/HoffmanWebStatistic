using System;
using System.Collections.Generic;

namespace MVCENG2.Models.Hoffman
{
    public partial class Operator
    {
        public Operator()
        {
            OperatorsStands = new HashSet<OperatorsStand>();
            ResultsJsonHeaders = new HashSet<ResultsJsonHeader>();
        }

        public int Id { get; set; }
        public string OLogin { get; set; } = null!;
        public string OPassword { get; set; } = null!;
        public string ODescription { get; set; } = null!;
        public DateTime Created { get; set; }
        public string InactiveMark { get; set; } = null!;

        public virtual ICollection<OperatorsStand> OperatorsStands { get; set; }
        public virtual ICollection<ResultsJsonHeader> ResultsJsonHeaders { get; set; }
    }
}
