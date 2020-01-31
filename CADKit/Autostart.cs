using Autofac;
using CADKit.Contracts;
using CADKit.Proxy;
using CADKit.Proxy.Runtime;
using System;
using System.Linq;
using System.Reflection;

namespace CADKit
{
    public class Autostart : IExtensionApplication
    {
        public static string AppName = "CADKit";

        public void Initialize()
        {
            DI.Container = Container.Builder.Build();

            var ass = AppDomain.CurrentDomain.GetAssemblies()
                .Where(x => x.FullName.StartsWith(AppName, StringComparison.OrdinalIgnoreCase));
            
            foreach (var tp in ass)
            {
                var t = tp.GetTypes().Where(x => x.GetTypeInfo().ImplementedInterfaces.Contains(typeof(IAutostart)));
                foreach (var i in t)
                {
                    try
                    {
                        var objectType = Type.GetType(i.AssemblyQualifiedName);
                        IAutostart instance = Activator.CreateInstance(objectType) as IAutostart;
                        instance.Initialize();
                    }
                    catch (System.Exception ex)
                    {
                        CADProxy.Editor.WriteMessage(ex.Message);
                    }
                } 
            }
        }

        public void Terminate()
        {
        }
    }
}
