﻿using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace umkm_webapp.Models
{
    public partial class Role
    {
        public Role()
        {
            RoleAccounts = new HashSet<RoleAccount>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }

        public virtual ICollection<RoleAccount> RoleAccounts { get; set; }
    }
}
