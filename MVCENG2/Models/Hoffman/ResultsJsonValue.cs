using System;
using System.Collections.Generic;

namespace MVCENG2.Models.Hoffman
{
    public partial class ResultsJsonValue
    {
        public long Id { get; set; }
        public string VName { get; set; } = null!;
        public string VValue { get; set; } = null!;
        public long TestId { get; set; }

        public virtual ResultsJsonTest Test { get; set; } = null!;
    }
}
