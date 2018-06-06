using System;
using System.Collections.Generic;
using System.Text;

namespace Cl.AuthorityManagement.Entity
{
    public class ModuleElementModule
    {
        public int ModuleElementID { get; set; }
        public int ModuleID { get; set; }

        public virtual ModuleElement ModuleElement { get; set; }
        public virtual Module Module { get; set; }
    }
}
