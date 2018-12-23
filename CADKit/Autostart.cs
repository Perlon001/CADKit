using CADKitCore.Contract;
using CADKitCore.Presenters;
using CADKitCore.Settings;
using CADKitCore.Util;
using CADKitCore.Views.WF;
using CADKitDALCAD;
using System.Windows.Forms;

[assembly: ZwSoft.ZwCAD.Runtime.ExtensionApplication(typeof(CADKitCore.Autostart))]

namespace CADKitCore
{
    public class Autostart : ZwSoft.ZwCAD.Runtime.IExtensionApplication
    {
        private AppSettings settings = AppSettings.Instance;
        public void Initialize()
        {
            
            CADProxy.Editor.WriteMessage("\nStart CADKitCore");

            // Create DI Container (load all CADKit*.dll modules)
            try
            {
                DI.Container = Container.Builder.Build();
            }
            catch (System.Exception ex)
            {
                CADProxy.Editor.WriteMessage("Błąd: \n" + ex.Message);
            }
            CADProxy.Editor.WriteMessage("\n");
            ISettingsView settingsView = new SettingsView();
            ISettingsPresenter settingsPresenter = new SettingsPresenter(settingsView);
            settings.CADKitPalette.Add("Ustawienia", settingsView as Control);
            settings.CADKitPalette.Visible = true;

        }

        public void Terminate()
        {
        }
    }
}
