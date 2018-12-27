using Autofac;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace CADKit.Util
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

            string CurrentDomainName = "CADKit";
            string CurrentDomainDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase).Remove(0, 6);

            foreach (string filePath in Directory.GetFiles(CurrentDomainDirectory, CurrentDomainName + "*.dll"))
            {
                AppDomain.CurrentDomain.Load(Path.GetFileNameWithoutExtension(filePath));
            }
            var assemblies = AppDomain.CurrentDomain.GetAssemblies()
               .Where(x => x.FullName.StartsWith(CurrentDomainName, StringComparison.Ordinal));

            // assemblies = assemblies.Reverse();

            foreach (var type in assemblies)
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
