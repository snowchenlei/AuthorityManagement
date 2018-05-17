using Autofac;
using Autofac.Extras.DynamicProxy;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using Cl.AuthorityManagement.Web.Controllers;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace Cl.AuthorityManagement.Web.App_Start
{
    public class AutoFacConfig
    {
        //internal static IContainer Container = null;
        public static IContainer Container { get; private set; }
        public static void Register()
        {
            ContainerBuilder builder = new ContainerBuilder();

            SetupResolveRules(builder);
            //注册api容器的实现
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            //注册mvc容器的实现
            builder.RegisterControllers(Assembly.GetExecutingAssembly());

            //Aop
            //builder.RegisterType<UserController>().EnableClassInterceptors();
            //builder.Register(c => new CallLogger());

            var container = builder.Build();
            Container = container;

            #region WebApi实现            
            //注意此处HttpConfiguration类的 config对象，一定不要new,要从GlobalConfiguration获取
            HttpConfiguration config = GlobalConfiguration.Configuration;
            //注意此处与MVC依赖注入不同
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            #endregion

            #region Mvc实现           
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            #endregion
            //return container;
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