﻿using Autofac;
using Cl.AuthorityManagement.Entity;
using Cl.AuthorityManagement.IServices;
using Cl.AuthorityManagement.Library.Mvc;
using Cl.AuthorityManagement.Web.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Cl.AuthorityManagement.Web.Controllers
{
    public class BaseController : Controller
    {
        protected UserInfo userInfo = null;
        protected string Controllername;   //当前控制器小写名称
        protected string Actionname;        //当前Action小写名称
        protected override JsonResult Json(object data, string contentType, Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            if (behavior == JsonRequestBehavior.DenyGet
                && string.Equals(this.Request.HttpMethod, "GET",
                                 StringComparison.OrdinalIgnoreCase))
                //Call JsonResult to throw the same exception as JsonResult
                return new JsonResult();
            return new JsonNetResult()
            {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding
            };
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            Controllername = Request.RequestContext.RouteData.Values["controller"].ToString();
            Actionname = filterContext.ActionDescriptor.ActionName.ToLower();
            
            userInfo = Session["LoginUser"] as UserInfo;
            if (userInfo == null)
            {
                filterContext.Result = new RedirectResult("/Account/Login");
                return;
            }
            else
            {
                //判断用户是否具有访问方法的权限
            }
        }
    }
}