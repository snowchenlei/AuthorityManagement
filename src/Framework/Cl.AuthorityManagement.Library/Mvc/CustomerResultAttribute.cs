using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Cl.AuthorityManagement.Library.Mvc
{
    public class CustomerResultAttribute : FilterAttribute, IResultFilter
    {
        public void OnResultExecuting(ResultExecutingContext filterContext)
        {
            string action = filterContext.RouteData.Values["action"]?.ToString();
            if (action.Equals("Add", StringComparison.InvariantCultureIgnoreCase))
            {
                filterContext.Controller.ViewBag.Action = "Add";
                filterContext.Controller.ViewBag.Operate = "添加";
            }
            else if (action.Equals("Edit", StringComparison.InvariantCultureIgnoreCase))
            {
                filterContext.Controller.ViewBag.Action = "Edit";
                filterContext.Controller.ViewBag.Operate = "修改";
            }
        }

        public void OnResultExecuted(ResultExecutedContext filterContext)
        {
        }
    }
}
