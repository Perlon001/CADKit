using Autofac;
using CADKit;
using CADKit.Contracts;
using CADKit.Proxy;
using CADKitBasic.Contracts;
using System.Windows.Forms;

namespace CADKitBasic
{
    public class ABCAutostart : IAutostart
    {
        public void Initialize()
        {
            AppSettings.Instance.CADKitPalette.Add("Ustawienia", DI.Container.Resolve<ISettingsView>() as Control);
            AppSettings.Instance.CADKitPalette.Visible = true;
            CADProxy.SetSystemVariable("CANNOSCALE", "1:100");
        }
    }
}
