using System.Web.Mvc;
using System.Web.Routing;
using MVCJob.JobBase;
using StackExchange.Profiling;
using StackExchange.Profiling.EntityFramework6;
using Autofac;
using Repository_基础结构层.Repository;
using Repository_基础结构层;
using System.Reflection;
using Autofac.Integration.Mvc;
using System.Web.Compilation;
using System.Linq;
using IOC.Server;

namespace MVCEchartsManager
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private static IContainer _container;  //声明一个容器
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            ProfileMVC.AutoMapper.Start();

            var builder = new ContainerBuilder();
            builder.RegisterGeneric(typeof(RepositoryBase<>)).As(typeof(IRepositoryBase<>)).PropertiesAutowired();
            builder.RegisterType(typeof(IOC.Server.ShopingServer)).As(typeof(IShopingServer)).PropertiesAutowired();

            //注册controller，使用属性注入
            builder.RegisterControllers(typeof(MvcApplication).Assembly).PropertiesAutowired();

            //注册所有的ApiControllers
            //builder.RegisterApiControllers(Assembly.GetCallingAssembly()).PropertiesAutowired();
            
            //注册程序集 
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).PropertiesAutowired();

            builder.RegisterModelBinders(typeof(MvcApplication).Assembly);
            builder.RegisterModelBinderProvider();
            
            // 注册所有的特性
            builder.RegisterFilterProvider();

            // //通过容器配置生成容器. 
            _container = builder.Build();

            ////提供给MVC
            DependencyResolver.SetResolver(new AutofacDependencyResolver(_container));

            JobManager.State();
            MiniProfilerEF6.Initialize();
        }

        protected void Application_BeginRequest()
        {
            if (Request.IsLocal)//这里是允许本地访问启动监控,可不写
            {
                MiniProfiler.Start();
            }
        }

        protected void Application_EndRequest()
        {
            MiniProfiler.Stop();
        }
    }
}
