using System;
using System.Collections.Generic;

namespace HoffmanWebstatistic.Models.Hoffman
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
