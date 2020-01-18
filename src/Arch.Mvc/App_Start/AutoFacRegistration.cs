using Arch.Infra.IoC;
using Autofac;
using Autofac.Configuration;
using Autofac.Integration.Mvc;
using Microsoft.Extensions.Configuration;
using StackExchange.Profiling;
using StackExchange.Profiling.Mvc;
using System.Web.Mvc;

namespace Arch.Mvc.App_Start
{
    public class AutoFacRegistration
    {
        private static ContainerBuilder _builder;
        public static void Initialize(ContainerBuilder builder)
        {
            _builder = builder;
            InitializeContainer();
        }

        private static void InitializeContainer()
        {
            _builder.RegisterControllers(typeof(MvcApplication).Assembly).InstancePerRequest();



            IConfigurationBuilder config = new ConfigurationBuilder();
            ConfigurationModule module = new ConfigurationModule(config.Build());

            ArchBootstrapperAutoFac.Register(_builder);

            _builder.RegisterModule(module);
            _builder.RegisterFilterProvider();

            IContainer container = _builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}