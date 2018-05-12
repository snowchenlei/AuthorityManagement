using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cl.AuthorityManagement.Entity
{
    /// <summary>
    /// 权限
    /// </summary>
    public class Permission : Entity
    {
        /// <summary>
        /// 权限名称
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }

        [JsonIgnore]
        public virtual ICollection<Role> Roles { get; set; }
    }
}
