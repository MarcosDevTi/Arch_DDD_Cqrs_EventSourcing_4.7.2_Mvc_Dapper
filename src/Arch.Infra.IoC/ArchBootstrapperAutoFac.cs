using Arch.CqrsHandlers.Customers;
using Arch.Infra.DataDapper.Sqlite;
using Arch.Infra.IoC.Extentions;
using Arch.Infra.Shared.Cqrs;
using Arch.Infra.Shared.Cqrs.Extentions;
using Arch.Infra.Shared.DomainNotifications;
using Autofac;
using Dapper;
using System;

namespace Arch.Infra.IoC
{
    public class ArchBootstrapperAutoFac
    {
        public static void Register(ContainerBuilder builder)
        {
            SqlMapper.AddTypeHandler(new MySqlGuidTypeHandler());
            SqlMapper.RemoveTypeMap(typeof(Guid));
            SqlMapper.RemoveTypeMap(typeof(Guid?));

            builder.RegisterType<Processor>().As<IProcessor>().InstancePerRequest();
            builder.RegisterType<DomainNotificationHandler>().As<IDomainNotification>().InstancePerRequest();
            builder.AddCqrsAutoFac<CustomerCommandHandler>();
            builder.RegisterType<DapperContext>();
            builder.RegisterType<EventSourcingDapperContext>().InstancePerRequest();
        }
    }
}
