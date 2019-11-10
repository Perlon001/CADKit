using Autofac;
using CADKit.Contracts;
using CADProxy;

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
            try
            {
                
                DI.Container = Container.Builder.Build();
                
                var settings = DI.Container.Resolve<AppSettings>();
                var view = DI.Container.Resolve<ISettingsView>();
                settings.CADKitPalette.Add("Ustawienia", view as System.Windows.Forms.Control);
                settings.GetSettingsFromDatabase();
                settings.SetSettingsToDatabase();
                settings.CADKitPalette.Visible = true;

                ProxyCAD.DocumentCreated -= OnDocumentCreated;
                ProxyCAD.DocumentCreated += OnDocumentCreated;
                ProxyCAD.DocumentDestroyed -= OnDocumentDestroyed;
                ProxyCAD.DocumentDestroyed += OnDocumentDestroyed;
            }
            catch (System.Exception ex)
            {
                ProxyCAD.Editor.WriteMessage("Błąd: \n" + ex.Message);
            }

            ProxyCAD.Editor.WriteMessage("\n");
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
