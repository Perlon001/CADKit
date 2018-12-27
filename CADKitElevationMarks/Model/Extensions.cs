using CADKit.ServiceCAD;
using ZwSoft.ZwCAD.DatabaseServices;
using ZwSoft.ZwCAD.Geometry;

namespace CADKitElevationMarks.Model
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
            if (CADProxy.Database.TileMode)
                return false;
            else
            {
                if (CADProxy.Database.PaperSpaceVportId == ObjectId.Null)
                    return false;
                else if (CADProxy.Editor.CurrentViewportObjectId == ObjectId.Null)
                    return false;
                else if (CADProxy.Editor.CurrentViewportObjectId == CADProxy.Database.PaperSpaceVportId)
                    return true;
                else
                    return false;
            }
        }

        public static BlockTableRecord GetBlockTableRecord(Transaction transaction, OpenMode mode)
        {
            BlockTable blockTable = (BlockTable)transaction.GetObject(CADProxy.Database.BlockTableId, OpenMode.ForRead);

            BlockTableRecord record;
            if (CADProxy.Database.TileMode == false && IsInLayoutPaper() == true)
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
