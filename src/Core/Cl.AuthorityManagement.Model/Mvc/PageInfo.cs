using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cl.AuthorityManagement.Model.Mvc
{
   public class PageInfo
    {
        public string Title { get; set; }
        public String Url { get; set; }
        /// <summary>
        /// 绝对地址
        /// </summary>
        public string AbsoluteUrl { get; set; }
        /// <summary>
        /// 布局页
        /// </summary>
        public string Layout { get; set; }
    }
}
