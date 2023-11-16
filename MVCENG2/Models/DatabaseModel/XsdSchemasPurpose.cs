using HoffmanWebstatistic.Models.Hoffman;
using System;
using System.Collections.Generic;

namespace HoffmanWebstatistic
{
    public partial class XsdSchemasPurpose
    {
        public XsdSchemasPurpose()
        {
            MesPathsCredentials = new HashSet<MesPathsCredential>();
            XsdSchemas = new HashSet<XsdSchema>();
        }

        public int Id { get; set; }
        public string XsdPurpose { get; set; } = null!;

        public virtual ICollection<MesPathsCredential> MesPathsCredentials { get; set; }
        public virtual ICollection<XsdSchema> XsdSchemas { get; set; }
    }
}
