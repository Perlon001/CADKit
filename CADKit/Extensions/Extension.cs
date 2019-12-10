using System.Reflection;

#if ZwCAD
using ZwSoft.ZwCAD.ApplicationServices;
#endif

#if AutoCAD
using Autodesk.AutoCAD.ApplicationServices;
#endif


namespace CADKit.Extensions
{
    public static class Extension
    {
        public static void ZoomExtens()
        {
            object acObject = Application.ZcadApplication;
            acObject.GetType().InvokeMember("ZoomExtents", BindingFlags.InvokeMethod, null, acObject, null);
        }
    }
}
