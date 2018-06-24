using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Http;
using Cl.AuthorityManagement.Web.App_Start;
using System.Web.Optimization;
using Cl.AuthorityManagement.Data;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core.Metadata.Edm;
using Cl.AuthorityManagement.Common.Logger;
using System.Threading;
using Cl.AuthorityManagement.Util;
using Cl.AuthorityManagement.Model.Logger;

namespace Cl.AuthorityManagement.Web
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // 在应用程序启动时运行的代码
            GlobalConfiguration.Configure(WebApiConfig.Register);
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //自定义注册
            AutoFacConfig.Register();
            AutoMapperConfig.Register();
            //取消响应头显示mvc版本信息
            MvcHandler.DisableMvcResponseHeader = true;
            
            //EF预热
            using (var dbcontext = new AuthorityManagementContext())
            {
                try
                {
                    var objectContext = ((IObjectContextAdapter)dbcontext).ObjectContext;
                    var mappingCollection = (StorageMappingItemCollection)objectContext.MetadataWorkspace.GetItemCollection(DataSpace.CSSpace);
                    mappingCollection.GenerateViews(new List<EdmSchemaError>());
                }
                catch (Exception ex)
                {
                    LoggerHelper.MvcError(ex.Message, ex);
                }
            }

            OperateData();
        }

        protected void Application_PreSendRequestHeaders()
        {
            Response.Headers.Remove("Server");          //Remove Server Header
            Response.Headers.Remove("X-AspNet-Version");//Remove X-AspNet-Version Header
        }

        public override string GetVaryByCustomString(HttpContext context, string custom)
        {
            if (custom == "Index_Key")
            {
                var flag = context.Cache["Index_Key"];
                if (flag == null)
                {
                    flag = DateTime.Now.Ticks;
                    context.Cache["Index_Key"] = flag;
                }
                return flag.ToString();
            }
            return base.GetVaryByCustomString(context, custom);
        }

        /// <summary>
        /// 定时队列扫描
        /// </summary>
        private void OperateData()
        {
            ThreadPool.QueueUserWorkItem(o =>
            {
                while (true)
                {
                    // 异常信息队列
                    if (Resource.ApiErrorQueue.Count > 0)
                    {
                        KeyValuePair<Exception, object> exception = Resource.ApiErrorQueue.Dequeue();
                        // 进行日志记录，新增了自定义信息
                        MonitorLog monLog = exception.Value as MonitorLog;
                        Exception excep = exception.Key;
                        LoggerHelper.ApiError(monLog, excep);
                    }
                    if (Resource.MvcErrorQueue.Count > 0)
                    {
                        KeyValuePair<Exception, object> exception = Resource.MvcErrorQueue.Dequeue();
                        MonitorLog monLog = exception.Value as MonitorLog;
                        Exception excep = exception.Key;
                        LoggerHelper.MvcError(monLog, excep);
                    }
                    Thread.Sleep(100);
                }
            });
        }
    }
}