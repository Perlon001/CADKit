using System;
using System.Collections.Generic;
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

        public static void ForEach<T>(this IEnumerable<T> _dict, Action<T> _action)
        {
            foreach (var item in _dict)
            {
                _action(item);
            }
        }
    }
}
