using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cl.AuthorityManagement.Model.Mvc
{
    public class ModuleElementEdit
    {
        public int? Id { get; set; }
        /// <summary>
	    /// Html元素 ID
	    /// </summary>
        [StringLength(50)]
        [Display(Name = "元素Id")]
        public string DomId { get; set; }

        /// <summary>
	    /// 名称
	    /// </summary>
        [Required]
        [StringLength(50)]
        [Display(Name = "名称")]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "方法")]
        public string Action { get; set; }

        /// <summary>
	    /// 类型
	    /// </summary>
        [StringLength(50)]
        [Display(Name = "元素类型")]
        public string Type { get; set; }

        /// <summary>
	    /// 元素附加属性
	    /// </summary>
        [StringLength(50)]
        [Display(Name = "附加属性")]
        public string Attr { get; set; }

        /// <summary>
	    /// 元素调用脚本
	    /// </summary>
        [StringLength(50)]
        [Display(Name = "事件名称")]
        public string Script { get; set; }

        /// <summary>
	    /// 元素图标
	    /// </summary>
        [StringLength(50)]
        [Display(Name = "元素图标")]
        public string Icon { get; set; }

        /// <summary>
	    /// 元素样式
	    /// </summary>
        [StringLength(50)]
        [Display(Name = "元素样式")]
        public string Class { get; set; }

        /// <summary>
	    /// 备注
	    /// </summary>
        [StringLength(50)]
        [Display(Name = "备注")]
        public string Remark { get; set; }

        /// <summary>
	    /// 排序字段
	    /// </summary>
        public int Sort { get; set; }
    }
}
