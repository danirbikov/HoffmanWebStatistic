using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HoffmanWebstatistic.Models.Hoffman
{
    public partial class Picture
    {
        [Key]
        public int Id { get; set; }
        public string PName { get; set; } = null!;
        public byte[] PictureBytes { get; set; } = null!;
    }
}
