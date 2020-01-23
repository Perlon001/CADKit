#if ZwCAD
using CADKit.Proxy;
using ZwSoft.ZwCAD.DatabaseServices;
#endif
#if AutoCAD
using Autodesk.AutoCAD.DatabaseServices;
#endif

namespace CADKit.Extensions
{
    public static class DBTextExtensions
    {
        public static void Flush(this DBText txt)
        {
            using (var tr = CADProxy.Document.TransactionManager.StartTransaction())
            {
                var btr = tr.GetObject(CADProxy.Document.Database.CurrentSpaceId, OpenMode.ForWrite) as BlockTableRecord;
                btr.AppendEntity(txt);
                tr.AddNewlyCreatedDBObject(txt, true);
                txt.Erase();
                tr.Commit();
            }
        }
    }
}
