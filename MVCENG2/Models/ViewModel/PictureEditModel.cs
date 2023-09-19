using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HoffmanWebstatistic.Models.Hoffman
{
    public partial class PictureEditModel
    {
        
        public int Id { get; set; }
        public string PName { get; set; } = null!;
        public List<IFormFile> PictureFile { get; set; } = null!;
    }
}
