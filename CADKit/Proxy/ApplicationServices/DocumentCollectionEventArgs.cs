using System;

#if ZwCAD
using CADApplicationServices = ZwSoft.ZwCAD.ApplicationServices;
using ZwSoft.ZwCAD.ApplicationServices;
#endif

#if AutoCAD
using CADApplicationServices = Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.ApplicationServices;
#endif

namespace CADKit.ApplicationServices
{
    public sealed class DocumentCollectionEventArgs : EventArgs
    {
        public Document Document { get; }
    }
}
