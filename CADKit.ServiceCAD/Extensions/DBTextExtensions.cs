#if ZwCAD
using ZwSoft.ZwCAD.DatabaseServices;
#endif
#if AutoCAD
using Autodesk.AutoCAD.DatabaseServices;
#endif

namespace CADProxy.Extensions
{
    public static class DBTextExtensions
    {
        public static void Flush(this DBText txt)
        {
            using (var tr = ProxyCAD.Document.TransactionManager.StartTransaction())
            {
                var btr = tr.GetObject(ProxyCAD.Document.Database.CurrentSpaceId, OpenMode.ForWrite) as BlockTableRecord;
                btr.AppendEntity(txt);
                tr.AddNewlyCreatedDBObject(txt, true);
                txt.Erase();
                tr.Commit();
            }
        }
    }
}
