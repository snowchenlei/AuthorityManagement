using Cl.AuthorityManagement.Common.Conversion;
using Cl.AuthorityManagement.Common.Http;
using Cl.AuthorityManagement.Common.Logger;
using Cl.AuthorityManagement.Model.Logger;
using Cl.AuthorityManagement.Util;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Cl.AuthorityManagement.Library.Api
{
    /// <summary>
    /// Api方法处理
    /// </summary>
    public class CustomerActionFilterAttribute: ActionFilterAttribute
    {
        private const string Key = "_thisWebApiOnActionMonitorLog_";
        /// <summary>
        /// 方法执行前
        /// </summary>
        /// <param name="actionContext"></param>
        public override async Task OnActionExecutingAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            if (!actionContext.ModelState.IsValid)
            {
                actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.BadRequest, actionContext.ModelState);
            }
            await base.OnActionExecutingAsync(actionContext, cancellationToken);

            MonitorLog MonLog = new MonitorLog();
            MonLog.ExecuteStartTime = DateTime.Now;
            //获取Action 参数
            MonLog.HttpMethod = actionContext.Request.Method.Method;
            if ("GET".Equals(MonLog.HttpMethod, StringComparison.InvariantCultureIgnoreCase))
            {
                MonLog.ActionParams = Serialization.SerializeObject(actionContext.ActionArguments);
            }
            //MonLog.ActionParams = new Dictionary<string, object>();//actionContext.ActionArguments;
            MonLog.HttpRequestHeaders = actionContext.Request.Headers.ToString();
            MonLog.IP = IPHelper.GetRealIP();

            actionContext.Request.Properties[Key] = MonLog;
            #region 如果参数是实体对象，获取序列化后的数据

            Stream stream = null;
            StreamReader reader = null;
            try
            {
                using (stream = await actionContext.Request.Content.ReadAsStreamAsync())
                {
                    stream.Position = 0;
                    using (reader = new StreamReader(stream, Encoding.UTF8))
                    {
                        string requestData = reader.ReadToEnd().ToString();
                        MonLog.ActionParams += requestData;
                        //if (!string.IsNullOrWhiteSpace(requestData) && !MonLog.ActionParams.ContainsKey("__EntityParamsList__"))
                        //{
                        //    MonLog.ActionParams["__EntityParamsList__"] = requestData;
                        //}
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (stream != null)
                {
                    stream.Dispose();
                }
                if (reader != null)
                {
                    reader.Dispose();
                }
            }

            #endregion

        }

        /// <summary>
        /// 方法执行后
        /// </summary>
        /// <param name="actionExecutedContext"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override async Task OnActionExecutedAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        {
            string responseBody = String.Empty;
            if (actionExecutedContext.Response == null)
            {
                responseBody = actionExecutedContext.Exception.Message;
            }
            else
            {
                responseBody = await actionExecutedContext.Response.Content.ReadAsStringAsync();
            }
            MonitorLog MonLog = actionExecutedContext.Request.Properties[Key] as MonitorLog;
            MonLog.ExecuteEndTime = DateTime.Now;
            MonLog.ActionName = actionExecutedContext.ActionContext.ActionDescriptor.ActionName;
            MonLog.ControllerName = actionExecutedContext.ActionContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            MonLog.ResponseData = responseBody;
            if(actionExecutedContext.Exception == null)
            {
                LoggerHelper.ApiMonitor(MonLog);
            }
            //if (actionExecutedContext.Response?.StatusCode == HttpStatusCode.OK)
            //{
            //    //string AesKey = Resource.AesKey;
            //    var encodeResult = AesEncryption.Encrypt(responseBody, AesKey);
            //    actionExecutedContext.Response.Content = new StringContent(encodeResult);
            //    await base.OnActionExecutedAsync(actionExecutedContext, cancellationToken);
            //}
        }
    }
}
