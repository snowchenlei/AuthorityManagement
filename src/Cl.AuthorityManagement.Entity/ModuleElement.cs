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
    /// 元素
    /// </summary>
    public class ModuleElement : BaseEntity, IComparable
    {
        public int Id { get; set; }
        /// <summary>
	    /// Html元素 ID
	    /// </summary>
        [StringLength(50)]
        public string DomId { get; set; }

        /// <summary>
	    /// 名称
	    /// </summary>
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        /// <summary>
	    /// 类型
	    /// </summary>
        [StringLength(50)]
        public string Type { get; set; }

        /// <summary>
	    /// 元素附加属性
	    /// </summary>
        [StringLength(50)]
        public string Attr { get; set; }

        /// <summary>
	    /// 元素调用脚本
	    /// </summary>
        [StringLength(50)]
        public string Script { get; set; }

        /// <summary>
	    /// 元素图标
	    /// </summary>
        [StringLength(50)]
        public string Icon { get; set; }

        /// <summary>
	    /// 元素样式
	    /// </summary>
        [StringLength(50)]
        public string Class { get; set; }

        /// <summary>
	    /// 备注
	    /// </summary>
        [StringLength(50)]
        public string Remark { get; set; }

        /// <summary>
	    /// 排序字段
	    /// </summary>
        public int Sort { get; set; }

        [JsonIgnore]
        public virtual ICollection<Module> Modules { get; set; }
        [JsonIgnore]
        public virtual ICollection<UserInfoModuleElement> UserInfos { get; set; }
        [JsonIgnore]
        public virtual ICollection<RoleModuleElement> RoleModuleElement { get; set; }

        public int CompareTo(object obj)
        {
            if(obj is null)
            {
                return 1;
            }
            if(obj is ModuleElement)
            {
                return Sort.CompareTo((obj as ModuleElement).Sort);
            }
            else
            {
                return 1;
            }            
        }
    }
}
