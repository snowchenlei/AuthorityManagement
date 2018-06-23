using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cl.AuthorityManagement.Library.Mvc
{
    public class CustomerResultAttribute : ResultFilterAttribute//IResultFilter
    {
        public override Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            string action = context.RouteData.Values["action"]?.ToString();
            if (action.Equals("Add", StringComparison.InvariantCultureIgnoreCase))
            {
                var controller = context.Controller as Controller;
                if (controller != null)
                {
                    controller.ViewBag.Action = "Add";
                    controller.ViewBag.Operate = "添加";
                }

                //var viewResult = context.Result as ViewResult; //Check also for PartialViewResult and ViewComponentResult
                //if (viewResult == null) return;
                //dynamic viewBag = new DynamicViewData(() => viewResult.ViewData);
                //viewBag.Message = "Foo message";
            }
            else if (action.Equals("Edit", StringComparison.InvariantCultureIgnoreCase))
            {
                var controller = context.Controller as Controller;
                if (controller != null)
                {
                    controller.ViewBag.Action = "Edit";
                    controller.ViewBag.Operate = "修改";
                }
            }

            return base.OnResultExecutionAsync(context, next);
        }
    }
}
