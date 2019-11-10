using Autofac;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

#if ZwCAD
using ApplicationServices = ZwSoft.ZwCAD.ApplicationServices;
#endif

#if AutoCAD
using ApplicationServices = Autodesk.AutoCAD.ApplicationServices;
#endif

namespace CADKit.DIContainer
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

            string product = (string)ApplicationServices.Application.GetSystemVariable("PRODUCT");
            string currentDomainName = "CADKit";
            string currentDomainDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase).Remove(0, 6);

            foreach (string filePath in Directory.GetFiles(currentDomainDirectory, currentDomainName + product + "*.dll")
                .Where(x => Path.GetFileNameWithoutExtension(x) != currentDomainName + product + ".dll"))
            {
                string fileName = Path.GetFileNameWithoutExtension(filePath);
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
