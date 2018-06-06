using System;
using System.Collections.Generic;
using System.Text;

namespace Cl.AuthorityManagement.Entity
{
    public class ModuleUserInfo
    {
        public int ModuleID { get; set; }
        public int UserInfoID { get; set; }

        public virtual Module Module { get; set; }
        public virtual UserInfo UserInfo { get; set; }
    }
}
