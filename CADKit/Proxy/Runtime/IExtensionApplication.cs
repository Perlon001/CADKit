#if ZwCAD
using CADRuntime = ZwSoft.ZwCAD.Runtime;
#endif

#if AutoCAD
using CADRuntime = Autodesk.AutoCAD.Runtime;
#endif

namespace CADKit.Runtime
{
    public interface IExtensionApplication : CADRuntime.IExtensionApplication
    {
    }
}
