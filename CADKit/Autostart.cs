using Autofac;
using CADKit.Contracts;
using CADProxy;
using System;
using System.Reflection;
using System.Windows.Forms;
using System.Linq;
using CADProxy.Runtime;

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
        public void Initialize()
        {
            DI.Container = Container.Builder.Build();

            AppSettings.Instance.CADKitPalette.Add("Ustawienia", DI.Container.Resolve<ISettingsView>() as Control);

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
                    catch (Exception ex)
                    {
                        ProxyCAD.Editor.WriteMessage(ex.Message);
                    }
                } 
            }

            AppSettings.Instance.CADKitPalette.Visible = true;

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
            AppSettings.Instance.GetSettingsFromDatabase();
            AppSettings.Instance.SetSettingsToDatabase();
        }

        void OnDocumentDestroyed(object sender, ApplicationServices.DocumentDestroyedEventArgs e)
        {
            if (ProxyCAD.Document == null)
            {
                AppSettings.Instance.Reset();
            }
        }
    }
}
