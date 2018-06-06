﻿using Cl.AuthorityManagement.Common.Http;
using Cl.AuthorityManagement.Common.Logger;
using Cl.AuthorityManagement.Model.Logger;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Cl.AuthorityManagement.Library.Mvc
{
    public class CustomerActionAttribute : IActionFilter
    {
        private const string Key = "_thisWebApiOnActionMonitorLog_";

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {            
            string responseBody = String.Empty;
            HttpRequest request = filterContext.HttpContext.Request;
            MonitorLog MonLog = request.HttpContext.Items[Key] as MonitorLog;
            MonLog.ExecuteEndTime = DateTime.Now;
            MonLog.ActionName = filterContext.RouteData.Values["action"] as string;
            MonLog.ControllerName = filterContext.RouteData.Values["controller"] as string;
            MonLog.ResponseData = responseBody;
            if (filterContext.Exception == null)
            {
                LoggerHelper.MVcMonitor(MonLog);
            }
            //if (actionExecutedContext.Response?.StatusCode == HttpStatusCode.OK)
            //{
            //    string AesKey = Resource.AesKey;
            //    var encodeResult = AesEncryption.Encrypt(responseBody, AesKey);
            //    actionExecutedContext.Response.Content = new StringContent(encodeResult);
            //    await base.OnActionExecutedAsync(actionExecutedContext, cancellationToken);
            //}
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            MonitorLog MonLog = new MonitorLog();
            MonLog.ExecuteStartTime = DateTime.Now;
            HttpRequest request = filterContext.HttpContext.Request;
            //获取Action 参数
            MonLog.ActionParams = Common.Conversion.Serialization.SerializeObject(filterContext.ActionArguments);
            MonLog.HttpRequestHeaders = HttpUtility.UrlDecode(request.Headers.ToString());
            MonLog.HttpMethod = request.Method;
            //MonLog.IP = IPHelper.GetRealIP();

            request.HttpContext.Items.Add(new KeyValuePair<object, object>(Key, MonLog));

            //filterContext.Controller.ViewData[Key] = MonLog;
            #region 如果参数是实体对象，获取序列化后的数据

            //Stream stream = null;
            //StreamReader reader = null;
            //try
            //{
            //    using (stream = await actionContext.Request.Content.ReadAsStreamAsync())
            //    {
            //        stream.Position = 0;
            //        using (reader = new StreamReader(stream, Encoding.UTF8))
            //        {
            //            string requestData = reader.ReadToEnd().ToString();
            //            if (!string.IsNullOrWhiteSpace(requestData) && !MonLog.ActionParams.ContainsKey("__EntityParamsList__"))
            //            {
            //                MonLog.ActionParams["__EntityParamsList__"] = requestData;
            //            }
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
            //finally
            //{
            //    if (stream != null)
            //    {
            //        stream.Dispose();
            //    }
            //    if (reader != null)
            //    {
            //        reader.Dispose();
            //    }
            //}

            #endregion
        }
    }
}
