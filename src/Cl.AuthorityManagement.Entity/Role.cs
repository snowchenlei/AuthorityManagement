﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cl.AuthorityManagement.Entity
{
    /// <summary>
    /// 角色
    /// </summary>
    public class Role : BaseEntity
    {
        public int Id { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }

        [JsonIgnore]
        public virtual ICollection<UserInfo> UserInfos { get; set; }
        [JsonIgnore]
        public virtual ICollection<Module> Modules { get; set; }
        [JsonIgnore]
        public virtual ICollection<RoleModuleElement> RoleModuleElements { get; set; }
    }
}
