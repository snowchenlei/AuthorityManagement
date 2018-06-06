using Autofac;
using Cl.AuthorityManagement.Common.Conversion;
using Cl.AuthorityManagement.Common.Extension;
using Cl.AuthorityManagement.Entity;
using Cl.AuthorityManagement.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Cl.AuthorityManagement.Web.Controllers
{
    public class BaseController : Controller
    {
        private readonly IUserInfoServices UserInfoServices = null;
        //public BaseController(IUserInfoServices userInfoServices)
        //{
        //    UserInfoServices = userInfoServices;
        //}

        protected UserInfo UserInfo = null;
        protected MethodInfo Function = null;
        protected string ControllerName;   //当前控制器小写名称
        protected string ActionName;        //当前Action小写名称
        
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            
            ControllerName = filterContext.RouteData.Values["controller"] as string;
            ActionName = filterContext.RouteData.Values["action"] as string;

            Function = this.GetType().GetMethods().FirstOrDefault(u => "index".Equals(u.Name, StringComparison.InvariantCultureIgnoreCase));
            if (Function == null)
                throw new Exception("未能找到Action");

            UserInfo = HttpContext.Session.Get<UserInfo>("LoginUser");
            if (UserInfo== null)
            {
                filterContext.Result = new RedirectResult("/Account/Login");
                return;
            }
#if DEBUG
            if (UserInfo == null)
            {
                //IUserInfoServices userInfoServices = AutoFacConfig.Container.Resolve<IUserInfoServices>();
                UserInfo = UserInfoServices.LoadFirst(u => u.UserName == "admin");
            }
#endif
        }
        
        protected virtual void Authorization(ref ActionExecutingContext filterContext)
        {
            //默认不处理
        }

        //protected override JsonResult Json(object data, string contentType, Encoding contentEncoding, JsonRequestBehavior behavior)
        //{
        //    if (behavior == JsonRequestBehavior.DenyGet
        //        && string.Equals(this.Request.Method, "GET", StringComparison.OrdinalIgnoreCase))
        //    {
        //        return new JsonResult();
        //    }
        //    return new JsonNetResult()
        //    {
        //        Data = data,
        //        ContentType = contentType,
        //        ContentEncoding = contentEncoding
        //    };
        //}

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