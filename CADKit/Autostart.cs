using CADKit.ServiceCAD;
using CADKit.Contract;
using CADKit.Presenters;
using CADKit.Settings;
using CADKit.Util;
using CADKit.Views.WF;
using System.Windows.Forms;

[assembly: ZwSoft.ZwCAD.Runtime.ExtensionApplication(typeof(CADKit.Autostart))]

namespace CADKit
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
        }

        public void Terminate()
        {
        }
    }
}
