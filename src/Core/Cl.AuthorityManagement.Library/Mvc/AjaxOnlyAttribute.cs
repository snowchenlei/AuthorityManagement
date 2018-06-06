using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cl.AuthorityManagement.Library.Mvc
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class AjaxOnlyAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// 只支持Ajax访问
        /// </summary>
        /// <param name="filterContext">The filter context.</param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //if (!filterContext.HttpContext.Request.IsAjaxRequest())
            //{
            //    filterContext.Result = new HttpNotFoundResult();
            //}
            base.OnActionExecuting(filterContext);
        }
    }
}
