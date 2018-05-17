using Autofac;
using Cl.AuthorityManagement.Entity;
using Cl.AuthorityManagement.Enum;
using Cl.AuthorityManagement.IServices;
using Cl.AuthorityManagement.Library.Mvc;
using Cl.AuthorityManagement.Web.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Cl.AuthorityManagement.Web.Controllers
{
    public class BaseController : Controller
    {
        protected UserInfo UserInfo = null;
        protected MethodInfo Function = null;
        protected string ControllerName;   //当前控制器小写名称
        protected string ActionName;        //当前Action小写名称

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            ControllerName = Request.RequestContext.RouteData.Values["controller"].ToString();
            ActionName = filterContext.ActionDescriptor.ActionName.ToLower();

            Function = this.GetType().GetMethods().FirstOrDefault(u => u.Name.ToLower() == ActionName);
            if (Function == null)
                throw new Exception("未能找到Action");

            UserInfo = Session["LoginUser"] as UserInfo;

#if DEBUG
            if (UserInfo == null)
            {
                IUserInfoServices userInfoServices = AutoFacConfig.Container.Resolve<IUserInfoServices>();
                UserInfo = userInfoServices.LoadFirst(u => u.UserName == "admin");
            }
#endif
            if (UserInfo == null)
            {
                filterContext.Result = new RedirectResult("/Manager/Account/Login");
                return;
            }
            else
            {
                Authorization(ref filterContext);
            }
        }
        
        protected virtual void Authorization(ref ActionExecutingContext filterContext)
        {
            //默认不处理
        }

        protected override JsonResult Json(object data, string contentType, Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            if (behavior == JsonRequestBehavior.DenyGet
                && string.Equals(this.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase))
            {
                return new JsonResult();
            }
            return new JsonNetResult()
            {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding
            };
        }

        protected IEnumerable<object> ModelStateToJson()
        {
            var errors = ModelState.Where(m => m.Value.Errors.Any())
                .Select(m => new
                {
                    m.Key,
                    Errors = String.Join("", m.Value.Errors.Select(e => e.ErrorMessage))
                });
            return errors;
        }
    }
}