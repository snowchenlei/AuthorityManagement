using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cl.AuthorityManagement.Enum
{
    public enum ApplicationType
    {
        [Display(Name = "接口")]
        Api = 1,
        [Display(Name = "网页")]
        Mvc,
        [Display(Name = "控制台")]
        Console
    }
}
