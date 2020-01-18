using Arch.CqrsHandlers.Customers;
using Arch.Infra.DataDapper.Sqlite;
using Arch.Infra.Shared.Cqrs;
using Arch.Infra.Shared.Cqrs.Extentions;
using Autofac;

namespace Arch.Infra.IoC
{
    public class ArchBootstrapperAutoFac
    {
        public static void Register(ContainerBuilder builder)
        {
            builder.RegisterType<Processor>().As<IProcessor>().InstancePerRequest();
            //builder.RegisterType<ArchCoreContext>().InstancePerRequest();

            builder.AddCqrsAutoFac<CustomerCommandHandler>();
            builder.RegisterType<DapperContext>();
        }
    }
}
