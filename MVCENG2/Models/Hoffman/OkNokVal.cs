using System;
using System.Collections.Generic;

namespace MVCENG2.Models.Hoffman
{
    public partial class OkNokVal
    {
        public OkNokVal()
        {
            ResultsJsonTests = new HashSet<ResultsJsonTest>();
        }

        public byte Id { get; set; }
        public string Val { get; set; } = null!;

        public virtual ICollection<ResultsJsonTest> ResultsJsonTests { get; set; }
    }
}
