using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cl.AuthorityManagement.Model.Mvc
{
    public class PermissionEdit
    {
        public int? Id { get; set; }

        [StringLength(50)]
        [Required]
        [Display(Name = "权限名")]
        public string Name { get; set; }

        [Display(Name = "排序")]
        public int Sort { get; set; }
    }
}
