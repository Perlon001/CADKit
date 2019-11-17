using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#if ZwCAD
using CADApplicationServices = ZwSoft.ZwCAD.ApplicationServices;
using ZwSoft.ZwCAD.ApplicationServices;
#endif

#if AutoCAD
using CADApplicationServices = Autodesk.AutoCAD.ApplicationServices;
Autodesk.AutoCAD.ApplicationServices;
#endif

namespace CADProxy.ApplicationServices
{
    public sealed class DocumentCollectionEventArgs : EventArgs
    {
        public Document Document { get; }
    }
}
