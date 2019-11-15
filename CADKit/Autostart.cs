using Autofac;
using CADKit.Contracts;
using CADProxy;
using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

using System.Linq;


#if ZwCAD
using ZwSoft.ZwCAD.Runtime;
using ApplicationServices = ZwSoft.ZwCAD.ApplicationServices;
#endif

#if AutoCAD
using Autodesk.AutoCAD.Runtime;
using ApplicationServices = Autodesk.AutoCAD.ApplicationServices;
#endif

[assembly: ExtensionApplication(typeof(CADKit.Autostart))]

namespace CADKit
{
    public class Autostart : IExtensionApplication
    {
        public void Initialize()
        {
            DI.Container = Container.Builder.Build();

            var settings = DI.Container.Resolve<AppSettings>();
            settings.CADKitPalette.Add("Ustawienia", DI.Container.Resolve<ISettingsView>() as Control);

            var ass = AppDomain.CurrentDomain.GetAssemblies()
                .Where(x => x.FullName.StartsWith(AppSettings.AppName, StringComparison.OrdinalIgnoreCase));
            
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
                        ProxyCAD.Editor.WriteMessage(ex.Message);
                    }
                } 
            }

            settings.CADKitPalette.Visible = true;

            ProxyCAD.DocumentCreated -= OnDocumentCreated;
            ProxyCAD.DocumentCreated += OnDocumentCreated;
            ProxyCAD.DocumentDestroyed -= OnDocumentDestroyed;
            ProxyCAD.DocumentDestroyed += OnDocumentDestroyed;
        }

        public void Terminate()
        {
        }

        void OnDocumentCreated(object sender, ApplicationServices.DocumentCollectionEventArgs e)
        {
            var settings = DI.Container.Resolve<AppSettings>();
            settings.GetSettingsFromDatabase();
            settings.SetSettingsToDatabase();
        }

        void OnDocumentDestroyed(object sender, ApplicationServices.DocumentDestroyedEventArgs e)
        {
            if (ProxyCAD.Document == null)
            {
                var settings = DI.Container.Resolve<AppSettings>();
                settings.Reset();
            }
        }
    }
}
