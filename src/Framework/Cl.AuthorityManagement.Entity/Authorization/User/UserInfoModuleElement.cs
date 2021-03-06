﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cl.AuthorityManagement.Entity
{
    public class UserInfoModuleElement : BaseModuleElement
    {
        [JsonIgnore]
        public virtual UserInfo UserInfo { get; set; }       
    }
}
