using CADKit.ServiceCAD;
using CADKitDALCAD;
using ZwSoft.ZwCAD.ApplicationServices;
using ZwSoft.ZwCAD.Runtime;

[assembly: ExtensionApplication(typeof(CADKitElevationMarks.Autostart))]

namespace CADKitElevationMarks
{
    public class Autostart : IExtensionApplication
    {
        public void Initialize()
        {
            CADProxy.Editor.WriteMessage("\nStart CADKitElevationMark");
        }

        public void Terminate()
        {
        }

    }
}
