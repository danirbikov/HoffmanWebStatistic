using System;
using System.Collections.Generic;

namespace HoffmanWebstatistic.Models.Hoffman
{
    public partial class XsdSchema2
    {
        public int Id { get; set; }
        public IFormFile XsdSchemaFile { get; set; } = null!;
        public string XsdDescription { get; set; } = null!;
        public int PurposeId { get; set; }
        public DateTime Created { get; set; }

        public virtual XsdSchemasPurpose Purpose { get; set; } = null!;
    }
}
