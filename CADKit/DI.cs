using Autofac;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using CADProxy;

namespace CADKit
{
    public static class DI
    {
        public static IContainer Container;
    }
    public static class Container
    {
        public static readonly ContainerBuilder Builder = CreateBuilder();

        private static ContainerBuilder CreateBuilder()
        {
            ContainerBuilder builder = new ContainerBuilder();

            string currentDomainName = "CADKit";
            string currentDomainDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase).Remove(0, 6);

            foreach (string filePath in Directory.GetFiles(currentDomainDirectory, currentDomainName + "*.dll")
                .Where(x => Path.GetFileNameWithoutExtension(x) != currentDomainName + CADProxy.ProxyCAD.Product + ".dll"))
            {
                AppDomain.CurrentDomain.Load(Path.GetFileNameWithoutExtension(filePath));
            }

            foreach (var type in AppDomain.CurrentDomain.GetAssemblies()
                .Where(x => x.FullName.StartsWith(currentDomainName, StringComparison.Ordinal)))
            {
                builder.RegisterAssemblyModules(type);
            }

            return builder;
        }
    }

    public static class TypeExtensions
    {
        public static bool IsAssignableTo<T>(this Type that)
        {
            return typeof(T).IsAssignableFrom(that);
        }

        public static bool CanBeInstantiated(this Type that)
        {
            return that.IsClass && !that.IsAbstract;
        }
    }
}
