using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cl.AuthorityManagement.Entity
{
    public class RoleModuleElement : BaseModuleElement
    {
        public int RoleID { get; set; }

        [JsonIgnore]
        public virtual Role Role { get; set; }
    }
}
