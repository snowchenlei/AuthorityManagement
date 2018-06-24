using Cl.AuthorityManagement.Library.Api;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Dispatcher;

namespace Cl.AuthorityManagement.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API 配置和服务

            #region 配置过滤器
            // 自定义请求处理
            //config.MessageHandlers.Add(new CustomerMessageHandler());

            //config.Filters.Add(new AuthorizationHandlingFilterAttribute());
            config.Filters.Add(new CustomerActionFilterAttribute());
            config.Filters.Add(new CustomerExceptionFilterAttribute());
            #endregion

            #region 替换默认的Json序列化方式
            //HttpConfiguration httpConfiguration = GlobalConfiguration.Configuration;
            //httpConfiguration.Formatters.Remove(httpConfiguration.Formatters.JsonFormatter);
            //httpConfiguration.Formatters.Insert(0, new JilFormatter());
            #endregion

            // Autofac IOC注册
            AutoFacConfig.Register();

            //删除返回xml格式
            config.Formatters.Remove(config.Formatters.XmlFormatter);

            // 在最后增加——使用CamelCase命名法序列化webApi的返回结果
            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            jsonFormatter.SerializerSettings.DateFormatString = "yyyy/MM/dd HH:mm:ss";

            // Web API 路由
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
