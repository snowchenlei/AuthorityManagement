using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cl.AuthorityManagement.Model.Mvc
{
    public class PushModel
    {
        [Required]
        [StringLength(50)]
        [Display(Name = "消息")]
        public string Message { get; set; }
    }
}
