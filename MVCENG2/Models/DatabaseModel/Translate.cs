using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HoffmanWebstatistic.Models.Hoffman
{
    
    public partial class Translate
    {
        [Key]        
        
        public string EngVariant { get; set; } = null!;
        public string RusVariant { get; set; } = null!;
    }
}
