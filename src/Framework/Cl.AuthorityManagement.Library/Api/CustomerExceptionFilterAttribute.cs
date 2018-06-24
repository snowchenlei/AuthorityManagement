using Cl.AuthorityManagement.Model.Logger;
using Cl.AuthorityManagement.Util;
using Qm.CardManagement.Model;
using Qm.CardManagement.Util;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Cl.AuthorityManagement.Library.Api
{
    /// <summary>
    /// Api异常处理
    /// </summary>
    public class CustomerExceptionFilterAttribute: ExceptionFilterAttribute
    {
        /// <summary>
        /// 发生异常时（同时记录请求报文数据）
        /// </summary>
        /// <param name="context"></param>
        public override void OnException(HttpActionExecutedContext context)
        {
            Exception exception = context.Exception;
            MonitorLog MonLog = null;
            if (context.Request.Properties.ContainsKey("_thisWebApiOnActionMonitorLog_"))
            {
                MonLog = context.Request.Properties["_thisWebApiOnActionMonitorLog_"] as MonitorLog;
            }
            else
            {
                //获取Action 参数
                MonLog = new MonitorLog();
                HttpActionContext actionContext = context.ActionContext;
                MonLog.ActionParams = Common.Conversion.Serialization.SerializeObject(actionContext.ActionArguments);
                MonLog.HttpRequestHeaders = actionContext.Request.Headers.ToString();
                MonLog.HttpMethod = actionContext.Request.Method.Method;
                MonLog.ActionName = actionContext.ActionDescriptor.ActionName;
                MonLog.ControllerName = actionContext.ActionDescriptor.ControllerDescriptor.ControllerName;
                MonLog.ResponseData = exception.Message;
            }            

            if (exception != null)
            {
                //将错误日志存入队列
                Resource.ApiErrorQueue.Enqueue(new KeyValuePair<Exception, object>(exception, MonLog));
            }
            if (exception is NotImplementedException)
            {
                context.Response = new HttpResponseMessage(HttpStatusCode.NotImplemented)
                {
                    Content = new StringContent("{\"State\":0,\"Message\":\"当前请求不受支持\"}", System.Text.Encoding.UTF8, "application/json"),
                };
            }
            else if (exception is TimeoutException)
            {
                context.Response = new HttpResponseMessage(HttpStatusCode.GatewayTimeout)
                {
                    Content = new StringContent("{\"State\":0,\"Message\":\"请求正忙，请稍后重试\"}", System.Text.Encoding.UTF8, "application/json"),
                };
            }
            else if (exception is SqlException)
            {
                context.Response = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent("{\"State\":0,\"Message\":\"数据库正忙，请稍后重试\"}", System.Text.Encoding.UTF8, "application/json"),
                };
            }
            else if (exception is WebException)
            {
                context.Response = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent("{\"State\":0,\"Message\":\"远程服务器正忙，请稍后重试\"}", System.Text.Encoding.UTF8, "application/json"),
                };
            }
            else if (exception is HttpRequestValidationException)
            {
                context.Response = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent("{\"State\":0,\"Message\":\"检测到非法字符，请核对后重试\"}", System.Text.Encoding.UTF8, "application/json"),
                };
            }
            else
            {
                context.Response = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent("{\"State\":0,\"Message\":\"服务器正忙，请稍后重试\"}", System.Text.Encoding.UTF8, "application/json"),
                };
            }
            context.Response.RequestMessage = context.Request;
            base.OnException(context);
        }
    }
}
