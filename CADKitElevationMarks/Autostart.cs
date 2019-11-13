using Autofac;
using CADKit;
using CADKit.Contracts;
using CADKitElevationMarks.Contracts.Views;
using System.Windows.Forms;

namespace CADKitElevationMarks
{
    public class Autostart : IAutostart
    {
        public void Initialize()
        {
            var settings = DI.Container.Resolve<AppSettings>();
            settings.CADKitPalette.Add("Koty wysokościowe", DI.Container.Resolve<IElevationMarksView>() as Control);
        }
    }
}
