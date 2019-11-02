using Autofac;
using CADKit.Contract;
using CADKit.DIContainer;
using CADKit.ServiceCAD;
using System.Windows.Forms;
using ZwSoft.ZwCAD.Runtime;

// [assembly: ExtensionApplication(typeof(CADKit.Autostart))]

namespace CADKit
{
    public class Autostart : IExtensionApplication
    {
        public void Initialize()
        {
            CADProxy.WriteMessage("\nStart CADKit");

            // Create DI Container (load all CADKit*.dll modules)
            try
            { 
                DI.Container = Container.Builder.Build();
                var settings = DI.Container.Resolve<AppSettings>();
                var view = DI.Container.Resolve<ISettingsView>();
                settings.CADKitPalette.Add("Ustawienia", view as Control);
                settings.CADKitPalette.Visible = true;

                DI.Container.Resolve<AppSettings>().GetSettingsFromDatabase();
                DI.Container.Resolve<AppSettings>().SetSettingsToDatabase();

                CADProxy.DocumentCreated -= OnDocumentCreated;
                CADProxy.DocumentCreated += OnDocumentCreated;
                CADProxy.DocumentDestroyed -= OnDocumentDestroyed;
                CADProxy.DocumentDestroyed += OnDocumentDestroyed;
            }
            catch (System.Exception ex)
            {
                CADProxy.WriteMessage("Błąd: \n" + ex.Message);
            }

            CADProxy.WriteMessage("\n");
        }

        public void Terminate()
        {
        }

        void OnDocumentCreated(object sender, ZwSoft.ZwCAD.ApplicationServices.DocumentCollectionEventArgs e)
        {
            DI.Container.Resolve<AppSettings>().GetSettingsFromDatabase();
            DI.Container.Resolve<AppSettings>().SetSettingsToDatabase();
        }

        void OnDocumentDestroyed(object sender, ZwSoft.ZwCAD.ApplicationServices.DocumentDestroyedEventArgs e)
        {
            if (CADProxy.Document == null)
            {
                DI.Container.Resolve<AppSettings>().Reset();
            }
        }
    }
}
