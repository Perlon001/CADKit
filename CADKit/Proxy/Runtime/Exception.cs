#if ZwCAD
using CADRuntime = ZwSoft.ZwCAD.Runtime;
#endif

#if AutoCAD
using CADRuntime = Autodesk.AutoCAD.Runtime;
#endif

namespace CADKit.Proxy.Runtime
{
    public class Exception : CADRuntime.Exception { }
}
