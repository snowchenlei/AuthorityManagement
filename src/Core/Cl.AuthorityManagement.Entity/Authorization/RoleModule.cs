using System;
using System.Collections.Generic;
using System.Text;

namespace Cl.AuthorityManagement.Entity
{
    public class RoleModule
    {
        public int RoleID { get; set; }
        public int MouleID { get; set; }

        public virtual Role Role { get; set; }
        public virtual Module Module { get; set; }
    }
}
