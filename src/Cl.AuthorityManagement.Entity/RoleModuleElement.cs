using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cl.AuthorityManagement.Entity
{
    public class RoleModuleElement
    {
        public int Id { get; set; }
        [JsonIgnore]
        public virtual Role Role { get; set; }
        [JsonIgnore]
        public virtual ModuleElement ModuleElement { get; set; }
        [JsonIgnore]
        public virtual Module Module { get; set; }
    }
}
