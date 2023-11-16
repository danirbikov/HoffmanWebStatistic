using HoffmanWebstatistic.Models.Hoffman;
using System;
using System.Collections.Generic;

namespace HoffmanWebstatistic
{
    public partial class MesPathsCredential
    {
        public int Id { get; set; }
        public int XsdPurposeId { get; set; }
        public string CPath { get; set; } = null!;
        public string CLogin { get; set; } = null!;
        public string CPassword { get; set; } = null!;

        public virtual XsdSchemasPurpose XsdPurpose { get; set; } = null!;
    }
}
