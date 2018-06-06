using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cl.AuthorityManagement.Model
{
    public class IPDetails
    {
        [Display(Name = "IP")]
        public string IP { get; set; }
        [Display(Name = "国家")]
        public string Country { get; set; }
        [Display(Name = "区域")]
        public string Area { get; set; }
        [Display(Name = "地区")]
        public string Region { get; set; }
        [Display(Name = "城市")]
        public string City { get; set; }
        [Display(Name = "县城")]
        public string County { get; set; }
        [Display(Name = "运营商")]
        public string Isp { get; set; }
    }
}
