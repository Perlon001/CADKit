using Autofac;
using CADKit;
using CADKit.Contracts;
using CADKitBasic.Contracts;
using System.Windows.Forms;

namespace CADKitBasic
{
    public class Autostart : IAutostart
    {
        public void Initialize()
        {
            AppSettings.Instance.CADKitPalette.Add("Ustawienia", DI.Container.Resolve<ISettingsView>() as Control);
            AppSettings.Instance.CADKitPalette.Visible = true;
        }
    }
}
