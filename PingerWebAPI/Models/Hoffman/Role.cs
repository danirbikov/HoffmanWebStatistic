﻿using System;
using System.Collections.Generic;
using PingerAPI.Models.General;

namespace PingerAPI.Models.Hoffman
{
    public partial class Role
    {
        public Role()
        {
            Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public string RName { get; set; } = null!;
        public string? RDescription { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}