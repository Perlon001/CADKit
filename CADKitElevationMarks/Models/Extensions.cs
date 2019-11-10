#if ZwCAD
using CADProxy;
using ZwSoft.ZwCAD.DatabaseServices;
using ZwSoft.ZwCAD.Geometry;
#endif

#if AutoCAD
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
#endif

namespace CADKitElevationMarks.Models
{
    public static class Extensions
    {
        public static void Store(this Entity ent, Transaction transaction, BlockTableRecord record, Group group, Matrix3d matrix)
        {
            ent.TransformBy(matrix);
            record.AppendEntity(ent);
            transaction.AddNewlyCreatedDBObject(ent, true);
            group.Append(ent.ObjectId);
        }


        public static bool IsInLayoutPaper()
        {
            if (ProxyCAD.Database.TileMode)
                return false;
            else
            {
                if (ProxyCAD.Database.PaperSpaceVportId == ObjectId.Null)
                    return false;
                else if (ProxyCAD.Editor.CurrentViewportObjectId == ObjectId.Null)
                    return false;
                else if (ProxyCAD.Editor.CurrentViewportObjectId == ProxyCAD.Database.PaperSpaceVportId)
                    return true;
                else
                    return false;
            }
        }

        public static BlockTableRecord GetBlockTableRecord(Transaction transaction, OpenMode mode)
        {
            BlockTable blockTable = (BlockTable)transaction.GetObject(ProxyCAD.Database.BlockTableId, OpenMode.ForRead);

            BlockTableRecord record;
            if (ProxyCAD.Database.TileMode == false && IsInLayoutPaper() == true)
            {
                record = (BlockTableRecord)transaction.GetObject(blockTable[BlockTableRecord.PaperSpace], mode);
            }
            else
            {
                record = (BlockTableRecord)transaction.GetObject(blockTable[BlockTableRecord.ModelSpace], mode);
            }

            return record;

        }
    }
}
