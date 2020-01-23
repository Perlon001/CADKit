using Autofac;
using System;
using System.Reflection;
using System.Linq;
using CADKit.Runtime;
using CADKit.Contracts;
using CADKit.Proxy;

#if ZwCAD
using ApplicationServices = ZwSoft.ZwCAD.ApplicationServices;
#endif

#if AutoCAD
using ApplicationServices = Autodesk.AutoCAD.ApplicationServices;
#endif

/*
    HKEY_CURRENT_USER\Software\ZWSOFT\ZWCAD\2020\en-US\Profiles\Default\Config\COLORSCHEME
    0 - interfejs ciemny
    1 - interfejs jasny
 */

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
                    catch (Exception ex)
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
