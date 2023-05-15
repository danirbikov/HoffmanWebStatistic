using System;
using System.Collections.Generic;

namespace MVCENG2.Models.Hoffman
{
    public partial class XsdSchemasPurpose
    {
        public XsdSchemasPurpose()
        {
            XsdSchemas = new HashSet<XsdSchema>();
        }

        public int Id { get; set; }
        public string XsdPurpose { get; set; } = null!;

        public virtual ICollection<XsdSchema> XsdSchemas { get; set; }
    }
}
