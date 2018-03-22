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
    /// 模块
    /// </summary>
    public class Module : BaseEntity, IComparable
    {
        public int Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// 请求地址
        /// </summary>
        [StringLength(500)]
        public string Url { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 节点图标文件名称
        /// </summary>
        [StringLength(50)]
        public string IconName { get; set; }

        [JsonIgnore]
        public virtual Module Parent { get; set; }
        [JsonIgnore]
        public virtual ICollection<Role> Roles { get; set; }
        [JsonIgnore]
        public virtual ICollection<UserInfo> UserInfos { get; set; }
        [JsonIgnore]
        public virtual ICollection<ModuleElement> ModuleElements { get; set; }
        [JsonIgnore]
        public virtual ICollection<UserInfoModuleElement> UserInfoModuleElement { get; set; }
        [JsonIgnore]
        public virtual ICollection<RoleModuleElement> RoleModuleElement { get; set; }

        public int CompareTo(object obj)
        {
            if (obj is null)
            {
                return 1;
            }
            if (obj is Module)
            {
                return Sort.CompareTo((obj as Module).Sort);
            }
            else
            {
                return 1;
            }
        }
    }
}
