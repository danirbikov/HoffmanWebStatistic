using System;
using System.Collections.Generic;

namespace HoffmanWebstatistic.Models.Hoffman
{
    public partial class ResultsJsonTest
    {
        public ResultsJsonTest()
        {
            ResultsJsonValues = new HashSet<ResultsJsonValue>();
        }

        public long Id { get; set; }
        public string TName { get; set; } = null!;
        public string TSpecname { get; set; } = null!;
        public byte ResId { get; set; }
        public DateTime Created { get; set; }
        public long HeaderId { get; set; }

        public virtual ResultsJsonHeader Header { get; set; } = null!;
        public virtual OkNokVal Res { get; set; } = null!;
        public virtual ICollection<ResultsJsonValue> ResultsJsonValues { get; set; }
    }
}
