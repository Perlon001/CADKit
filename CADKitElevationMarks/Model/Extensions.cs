using ZwSoft.ZwCAD.ApplicationServices;
using ZwSoft.ZwCAD.DatabaseServices;
using ZwSoft.ZwCAD.EditorInput;
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
            Document acDoc = Application.DocumentManager.MdiActiveDocument;
            Database acDatabase = acDoc.Database;
            Editor editor = acDoc.Editor;

            if (acDatabase.TileMode)
                return false;
            else
            {
                if (acDatabase.PaperSpaceVportId == ObjectId.Null)
                    return false;
                else if (editor.CurrentViewportObjectId == ObjectId.Null)
                    return false;
                else if (editor.CurrentViewportObjectId == acDatabase.PaperSpaceVportId)
                    return true;
                else
                    return false;
            }
        }

        public static BlockTableRecord GetBlockTableRecord(Transaction transaction, OpenMode mode)
        {
            BlockTable blockTable = (BlockTable)transaction.GetObject(Application.DocumentManager.MdiActiveDocument.Database.BlockTableId, OpenMode.ForRead);

            BlockTableRecord record;
            if (Application.DocumentManager.MdiActiveDocument.Database.TileMode == false && IsInLayoutPaper() == true)
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
