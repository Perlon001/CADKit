using Autofac;
using CADKit;
using CADKit.Contracts;
using CADKit.Proxy;
using CADKitBasic.Contracts;
using System.Windows.Forms;

namespace CADKitBasic
{
    public class Autostart : IAutostart
    {
        public void Initialize()
        {
            AppSettings.Get.CADKitPalette.Add("Ustawienia", DI.Container.Resolve<ISettingsView>() as Control);
            AppSettings.Get.CADKitPalette.Visible = true;
            CADProxy.SetSystemVariable("CANNOSCALE", "1:100");
        }
    }
}
