#if ZwCAD
using CADApplicationServices = ZwSoft.ZwCAD.ApplicationServices;
#endif

#if AutoCAD
using CADApplicationServices = Autodesk.AutoCAD.ApplicationServices;
#endif

namespace CADKit.Proxy.ApplicationServices
{
    public delegate void SystemVariableChangedEventHandler(object sender, CADApplicationServices.SystemVariableChangedEventArgs e);
}
