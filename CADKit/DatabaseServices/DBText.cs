#if ZwCAD
using CADDatabaseServices = ZwSoft.ZwCAD.DatabaseServices;
#endif
#if AutoCAD
using CADDatabaseServices = Autodesk.AutoCAD.DatabaseServices;
#endif

namespace CADProxy.DatabaseServices
{
    public class DBText : CADDatabaseServices.DBText
    {
        public DBText() : base() { }
    }
}
