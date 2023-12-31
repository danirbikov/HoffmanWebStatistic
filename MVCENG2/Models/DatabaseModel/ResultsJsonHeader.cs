﻿using System;
using System.Collections.Generic;

namespace HoffmanWebstatistic.Models.Hoffman
{
    public partial class ResultsJsonHeader
    {
        public ResultsJsonHeader()
        {
            ResultsJsonTests = new HashSet<ResultsJsonTest>();
        }

        public long Id { get; set; }
        public string VIN { get; set; } = null!;
        public string Ordernum { get; set; } = null!;
        public string JsonFilename { get; set; } = null!;
        public int StandId { get; set; }
        public DateTime Created { get; set; }
        public int OperatorId { get; set; }

        public virtual Operator Operator { get; set; } = null!;
        public virtual Stand Stand { get; set; } = null!;
        public virtual ICollection<ResultsJsonTest> ResultsJsonTests { get; set; }
    }
}
