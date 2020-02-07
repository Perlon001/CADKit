using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#if ZwCAD
using CADDatabaseServices = ZwSoft.ZwCAD.DatabaseServices;
#endif
#if AutoCAD
using CADDatabaseServices = Autodesk.AutoCAD.DatabaseServices;
#endif

namespace CADKit.Proxy.DatabaseServices
{
    public abstract class Entity : CADDatabaseServices.Entity
    {
        protected internal Entity(IntPtr unmanagedObjPtr, bool autoDelete) : base(unmanagedObjPtr, autoDelete)
        {
        }
    }
}
