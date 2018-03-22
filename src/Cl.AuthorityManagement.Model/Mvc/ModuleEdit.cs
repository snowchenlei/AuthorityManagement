using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cl.AuthorityManagement.Model.Mvc
{
    public class ModuleEdit
    {
        public int? Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        [StringLength(50)]
        [Display(Name = "模块名称")]
        public string Name { get; set; }

        /// <summary>
        /// 请求地址
        /// </summary>
        [StringLength(500)]
        [Display(Name = "请求地址")]
        public string Url { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [Display(Name = "排序")]
        public int Sort { get; set; }

        /// <summary>
        /// 节点图标文件名称
        /// </summary>
        [StringLength(50)]
        [Display(Name = "图标名称")]
        public string IconName { get; set; }

        [Display(Name = "父模块")]
        public int ParentId { get; set; }
    }
}
