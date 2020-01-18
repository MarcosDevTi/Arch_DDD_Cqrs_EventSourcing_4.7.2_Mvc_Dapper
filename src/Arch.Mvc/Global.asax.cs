using Arch.Mvc.App_Start;
using Autofac;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Arch.Mvc
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AutoFacRegistration.Initialize(new ContainerBuilder());
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
