using Autofac;
using CADKit;
using CADKit.Contracts;
using CADKitElevationMarks.Contracts.Views;
using CADKitElevationMarks.Views;
using CADProxy;
using System;
using System.Windows.Forms;
using ZwSoft.ZwCAD.ApplicationServices;

namespace CADKitElevationMarks
{
    public class Autostart : IAutostart
    {
        public void Initialize()
        {
            var control = DI.Container.Resolve<IElevationMarksView>() as Control;
            AppSettings.Instance.CADKitPalette.Add("Koty wysokościowe", control);
            ProxyCAD.Document.CommandCancelled += TestCanceled;
        }

        private void TestCanceled(object sender, CommandEventArgs e)
        {
            ProxyCAD.Editor.WriteMessage("Cancel {0}", e.GlobalCommandName);
        }
    }
}
