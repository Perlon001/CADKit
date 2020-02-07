using CADKit.Runtime;
using CADKit.Proxy;

#if ZwCAD
using ZwSoft.ZwCAD.ApplicationServices;
using ZwSoft.ZwCAD.EditorInput;
using ZwSoft.ZwCAD.DatabaseServices;
using ZwSoft.ZwCAD.Geometry;
#endif

#if AutoCAD
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
#endif

namespace CADKitBasic
{
    public static class CommandSplineMultiLeader
    {
        public static ObjectId GetArrowObjectId(string newArrName)
        {
            ObjectId arrObjId = ObjectId.Null;
            // Get the current value of DIMBLK
            string oldArrName = Application.GetSystemVariable("DIMBLK") as string;
            // Set DIMBLK to the new style
            // (this action may create a new block)
            Application.SetSystemVariable("DIMBLK", newArrName);
            // Reset the previous value of DIMBLK
            if (oldArrName.Length != 0)
                Application.SetSystemVariable("DIMBLK", oldArrName);
            // Now get the objectId of the block
            using (Transaction tr = CADProxy.Database.TransactionManager.StartTransaction())
            {
                BlockTable bt = (BlockTable)tr.GetObject(CADProxy.Database.BlockTableId, OpenMode.ForRead);
                arrObjId = bt[newArrName];
                tr.Commit();
            }
            return arrObjId;
        }

        [CommandMethod("mldspl")]
        public static void CreateMultiLeader()
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            const string arrowName = "_DOT";
            ObjectId arrId = GetArrowObjectId(arrowName);
            // Get the start point of the leader
            PromptPointResult result = CADProxy.Editor.GetPoint("\nSpecify leader arrowhead location: ");
            if (result.Status != PromptStatus.OK)
                return;
            Point3d startPt = result.Value;
            // Get the end point of the leader
            PromptPointOptions opts = new PromptPointOptions("\nSpecify landing location: ")
            {
                BasePoint = startPt,
                UseBasePoint = true
            };
            result = CADProxy.Editor.GetPoint(opts);
            if (result.Status != PromptStatus.OK)
                return;
            Point3d endPt = result.Value;
            using (Transaction tr = CADProxy.Database.TransactionManager.StartTransaction())
            {
                try
                {
                    BlockTable bt = (BlockTable)tr.GetObject(CADProxy.Database.BlockTableId, OpenMode.ForRead);
                    BlockTableRecord btr = (BlockTableRecord)tr.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite);
                    // Create the MLeader
                    MLeader mld = new MLeader();
                    int ldNum = mld.AddLeader();
                    int lnNum = mld.AddLeaderLine(ldNum);
                    mld.AddFirstVertex(lnNum, startPt);
                    mld.AddLastVertex(lnNum, endPt);
                    mld.ArrowSymbolId = arrId;
                    mld.LeaderLineType = LeaderType.SplineLeader;
                    // Create the MText
                    //TODO: Include interaction MText
                    MText mt = new MText
                    {
                        Contents = "Multi-line leader\n" + "with the \"" + arrowName + "\" arrow head",
                        Location = endPt,
                    };
                    mld.ContentType = ContentType.MTextContent;
                    mld.MText = mt;
                    // Add the MLeader
                    btr.AppendEntity(mld);
                    tr.AddNewlyCreatedDBObject(mld, true);
                    tr.Commit();
                }
                catch
                {
                    // Would also happen automatically
                    // if we didn't commit
                    tr.Abort();
                }
            }
        }
    }
}
