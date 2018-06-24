using Autofac;
using Autofac.Integration.WebApi;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;

namespace Cl.AuthorityManagement.Api
{
    public class AutoFacConfig
    {
        public static void Register()
        {
            ContainerBuilder builder = new ContainerBuilder();
            SetupResolveRules(builder);
            //RegisterApiControllers方法
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            var container = builder.Build();

            //注意此处HttpConfiguration类的 config对象，一定不要new,要从GlobalConfiguration获取
            HttpConfiguration config = GlobalConfiguration.Configuration;
            //注意此处与MVC依赖注入不同
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
        private static void SetupResolveRules(ContainerBuilder builder)
        {
            var iServices = Assembly.Load(ConfigurationManager.AppSettings["IServicesAssemblyString"]);
            var services = Assembly.Load(ConfigurationManager.AppSettings["ServicesAssemblyString"]);
            var iRepository = Assembly.Load(ConfigurationManager.AppSettings["IRepositoryAssemblyString"]);
            var repository = Assembly.Load(ConfigurationManager.AppSettings["RepositoryAssemblyString"]);

            //根据名称约定（服务层的接口和实现均以Services结尾），实现服务接口和服务实现的依赖
            builder.RegisterAssemblyTypes(iServices, services)
              .Where(t => t.Name.EndsWith("Services"))
              .AsImplementedInterfaces();
            ////根据名称约定（数据访问层的接口和实现均以Repository结尾），实现数据访问接口和数据访问实现的依赖
            builder.RegisterAssemblyTypes(iRepository, repository)
              .Where(t => t.Name.EndsWith("Repository"))
              .AsImplementedInterfaces();
        }
    }
}