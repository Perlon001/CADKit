using Autofac;
using CADKit;
using CADKit.Contracts;
using CADKitElevationMarks.Contract.Services;
using CADKitElevationMarks.Contracts.Views;
using CADKitElevationMarks.Services;
using System.Windows.Forms;

namespace CADKitElevationMarks
{
    public class Autostart : IAutostart
    {
        public void Initialize()
        {
            AppSettings.Get.CADKitPalette.Add("Koty wysokościowe", DI.Container.Resolve<IElevationMarksView>() as Control);
            AppSettings.Get.CADKitPalette.Visible = true;
            using(var scope = DI.Container.BeginLifetimeScope())
            {
                var markService = scope.Resolve<IMarkService>();
                foreach(var item in markService.GetMarks())
                {
                    var typ = item.markType;
                    //AppSettings.Get.PropertyService.AddComponent(;
                }
            }
        }
    }
}
