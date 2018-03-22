using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cl.AuthorityManagement.Entity
{
    public class UserInfoModuleElement
    {
        public int Id { get; set; }
        [JsonIgnore]
        public virtual UserInfo UserInfo { get; set; }
        [JsonIgnore]
        public virtual ModuleElement ModuleElement { get; set; }
        [JsonIgnore]
        public virtual Module Module { get; set; }
    }
}
