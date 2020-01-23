#if ZwCAD
using CADInternal = ZwSoft.ZwCAD.Internal;
#endif
#if AutoCAD
using CADInternal = Autodesk.AutoCAD.Internal;
#endif

namespace CADKit.Internal
{
    public class Utils : CADInternal.Utils
    {
        public Utils() : base() { }

        #if ZwCAD
        public static void FlushGraphics()
        {
            SynchZcadDisplay();
        }
        #endif
    }
}
