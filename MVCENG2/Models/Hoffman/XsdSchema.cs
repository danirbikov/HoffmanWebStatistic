using System;
using System.Collections.Generic;

namespace MVCENG2.Models.Hoffman
{
    public partial class XsdSchema
    {
        public int Id { get; set; }
        public byte[] XsdSchema1 { get; set; } = null!;
        public string XsdDescription { get; set; } = null!;
        public int PurposeId { get; set; }
        public DateTime Created { get; set; }

        public virtual XsdSchemasPurpose Purpose { get; set; } = null!;
    }
}
