using Autofac;
using CADKit;
using CADKit.Contracts;
using CADKitBasic.Contracts;
using CADKitElevationMarks.Contract.Services;
using CADKitElevationMarks.Contracts.Views;
using System.Windows.Forms;

namespace CADKitElevationMarks
{
    public class Autostart : IAutostart
    {
        public void Initialize()
        {
            const string title = "Koty wysokościowe";
            AppSettings.Get.CADKitPalette.Add(title, DI.Container.Resolve<IElevationMarksView>() as Control);
            AppSettings.Get.CADKitPalette.Visible = true;
            using (var scope = DI.Container.BeginLifetimeScope())
            {
                var controlPage = AppSettings.Get.CADKitPalette.GetPage("Ustawienia") as ISettingsView;
                controlPage.BindingComponents(title, scope.Resolve<IMarkService>().GetComponents());
            }
        }
    }
}
