using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cl.AuthorityManagement.Entity
{
    public class LoginInfo : Entity
    {

        [StringLength(50)]
        public string IP { get; set; }

        public string Device { get; set; }
        public string OS { get; set; }
        public string UserAgent { get; set; }

        [JsonIgnore]
        public UserInfo UserInfo { get; set; }
    }
}
