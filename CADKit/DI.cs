using Autofac;
using CADKit.Proxy;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

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

            string currentDomainDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase).Remove(0, 6);

            var files = Directory.GetFiles(currentDomainDirectory, Autostart.AppName + "*.dll")
                .Where(x => !Path.GetFileNameWithoutExtension(x).Equals(Autostart.AppName + CADProxy.Product, StringComparison.OrdinalIgnoreCase));
            foreach (string filePath in files)
            {
                CADProxy.Editor.WriteMessage("\nŁadowanie " + Path.GetFileNameWithoutExtension(filePath) + "...");
                AppDomain.CurrentDomain.Load(Path.GetFileNameWithoutExtension(filePath));
            }

            var ass = AppDomain.CurrentDomain.GetAssemblies()
                .Where(x => x.FullName.StartsWith(Autostart.AppName, StringComparison.Ordinal));
            foreach (Assembly type in ass)
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
