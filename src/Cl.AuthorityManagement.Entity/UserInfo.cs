using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cl.AuthorityManagement.Entity
{
    /// <summary>
    /// 用户
    /// </summary>
    public class UserInfo:BaseEntity
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        [Required]
        [StringLength(50)]
        [Description("用户名")]
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Required]
        [StringLength(50)]
        [Description("密码")]
        public string Password { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        [StringLength(11, MinimumLength = 11)]
        [Description("手机号")]
        public string PhoneNumber { get; set; }

        public bool IsCanUse { get; set; }

        [JsonIgnore]
        public virtual ICollection<Role> Roles { get; set; }
        [JsonIgnore]
        public virtual ICollection<Module> Modules { get; set; }
        [JsonIgnore]
        public virtual ICollection<UserInfoModuleElement> UserInfoModuleElements { get; set; }
    }
}
