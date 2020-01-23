using Autofac;
using CADKit;
using CADKit.Contracts;
using CADKit.Proxy;
using CADKitElevationMarks.Contracts.Views;
using System.Windows.Forms;

#if ZwCAD
using ZwSoft.ZwCAD.ApplicationServices;
#endif

#if AutoCAD
using Autodesk.AutoCAD.ApplicationServices;
#endif

namespace CADKitElevationMarks
{
    public class Autostart : IAutostart
    {
        public void Initialize()
        {
            var control = DI.Container.Resolve<IElevationMarksView>() as Control;
            AppSettings.Instance.CADKitPalette.Add("Koty wysokościowe", control);
            CADProxy.Document.CommandCancelled += TestCanceled;
        }

        private void TestCanceled(object sender, CommandEventArgs e)
        {
            CADProxy.Editor.WriteMessage("Cancel {0}", e.GlobalCommandName);
        }
    }
}
