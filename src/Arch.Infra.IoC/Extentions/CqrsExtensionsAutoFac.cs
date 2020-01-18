using Arch.Infra.Shared.Cqrs.Commands;
using Arch.Infra.Shared.Cqrs.Query;
using Autofac;
using System;
using System.Linq;
using System.Reflection;

namespace Arch.Infra.Shared.Cqrs.Extentions
{
    public static class CqrsExtensionsAutoFac
    {
        public static void AddCqrsAutoFac<T>(this ContainerBuilder builder, Func<AssemblyName, bool> filter = null)
        {
            var handlers = new[] { typeof(IQueryHandler<,>), typeof(ICommandHandler<>) };
            var target = typeof(T).Assembly;
            bool FilterTrue(AssemblyName x) => true;

            var assemblies = target.GetReferencedAssemblies()
                .Where(filter ?? FilterTrue)
                .Select(Assembly.Load)
                .ToList();
            assemblies.Add(target);

            var types = from t in assemblies.SelectMany(a => a.GetExportedTypes())
                        from i in t.GetInterfaces()
                        where i.IsConstructedGenericType &&
                              handlers.Contains(i.GetGenericTypeDefinition())
                        select new { i, t };

            foreach (var tp in types)
                builder.RegisterType(tp.t).As(tp.i);
        }
    }
}
