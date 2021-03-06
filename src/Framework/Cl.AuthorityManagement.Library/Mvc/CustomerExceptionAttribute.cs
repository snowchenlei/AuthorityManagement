﻿using Cl.AuthorityManagement.Common.Http;
using Cl.AuthorityManagement.Model.Logger;
using Cl.AuthorityManagement.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Cl.AuthorityManagement.Library.Mvc
{
    /// <summary>
    /// Mvc异常处理
    /// </summary>
    public class CustomerExceptionAttribute : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            Exception exception = filterContext.Exception;
            //异常是否处理
            if (filterContext.ExceptionHandled == true)
            {
                return;
            }

            MonitorLog MonLog = null;
            if (filterContext.Controller.ViewData.ContainsKey("_thisWebApiOnActionMonitorLog_"))
            {
                MonLog = filterContext.Controller.ViewData["_thisWebApiOnActionMonitorLog_"] as MonitorLog;
            }
            else
            {
                HttpRequestBase request = filterContext.HttpContext.Request;
                //获取Action 参数
                MonLog = new MonitorLog()
                {
                    HttpRequestHeaders = request.Headers.ToString(),
                    HttpMethod = request.HttpMethod,
                    IP = IPHelper.GetRealIP(),
                    ExecuteEndTime = DateTime.Now,
                    ActionName = filterContext.RouteData.Values["action"] as string,
                    ControllerName = filterContext.RouteData.Values["controller"] as string,
                    ResponseData = exception.Message
                    //ActionParams = request.Params.
                };
            }

            //加入队列
            Resource.MvcErrorQueue.Enqueue(new KeyValuePair<Exception, object>(exception, MonLog));
            HttpException httpException = new HttpException(null, exception);

            /*
             * 1、根据对应的HTTP错误码跳转到错误页面
             * 2、先对Action方法里引发的HTTP 404/400错误进行捕捉和处理
             * 3、其他错误默认为HTTP 500服务器错误
             */
            if (httpException != null && (httpException.GetHttpCode() == 400 || httpException.GetHttpCode() == 404))
            {
                filterContext.HttpContext.Response.StatusCode = 404;
                filterContext.HttpContext.Response.WriteFile("~/HttpError/404.html");
            }
            else
            {
                filterContext.HttpContext.Response.StatusCode = 500;
                filterContext.HttpContext.Response.WriteFile("~/HttpError/500.html");
            }

            //设置异常已经处理,否则会被其他异常过滤器覆盖
            filterContext.ExceptionHandled = true;

            //在派生类中重写时，获取或设置一个值，该值指定是否禁用IIS自定义错误。
            filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
        }
    }
}
