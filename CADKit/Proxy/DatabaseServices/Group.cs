using System;

#if ZwCAD
using CADDatabaseServices = ZwSoft.ZwCAD.DatabaseServices;
#endif
#if AutoCAD
using CADDatabaseServices = Autodesk.AutoCAD.DatabaseServices;
#endif

namespace CADKit.DatabaseServices
{
    public class Group : CADDatabaseServices.Group
    {
        public Group() : base() { }
        public Group(string description, bool selectable) : base(description, selectable) { }
        protected internal Group(IntPtr unmanagedPointer, bool autoDelete) : base(unmanagedPointer, autoDelete) { }
    }
}
