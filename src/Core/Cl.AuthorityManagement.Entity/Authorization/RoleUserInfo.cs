using System;
using System.Collections.Generic;
using System.Text;

namespace Cl.AuthorityManagement.Entity
{
    public class RoleUserInfo
    {
        public int UserInfoID { get; set; }
        public int RoleID { get; set; }

        public virtual UserInfo UserInfo { get; set; }
        public virtual Role Role { get; set; }
    }
}
