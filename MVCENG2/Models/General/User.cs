using System;
using System.Collections.Generic;
using HoffmanWebstatistic.Models.Hoffman;

namespace HoffmanWebstatistic.Models.General
{
    public partial class User
    {
        public int Id { get; set; }
        public string ULogin { get; set; } = null!;
        public string UPassword { get; set; } = null!;
        public int RoleId { get; set; }
        public DateTime Created { get; set; }
        public string InactiveMark { get; set; } = null!;

        public virtual Role Role { get; set; } = null!;
    }
}
