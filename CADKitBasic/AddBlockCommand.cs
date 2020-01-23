using ZwSoft.ZwCAD.ApplicationServices;
using ZwSoft.ZwCAD.DatabaseServices;
using ZwSoft.ZwCAD.Geometry;
using ZwSoft.ZwCAD.Runtime;

namespace CADKitBasic
{
    public class AddBlockCommand
    {
        [CommandMethod("AddBlockTest")]
        public static void AddBlockTest()
        {
            Database db = Application.DocumentManager.MdiActiveDocument.Database;
            using (Transaction myT = db.TransactionManager.StartTransaction())
            {
                //Get the block definition "Check".
                string blockName = "CHECK";
                BlockTable bt = db.BlockTableId.GetObject(OpenMode.ForRead) as BlockTable;
                BlockTableRecord blockDef = bt[blockName].GetObject(OpenMode.ForRead) as BlockTableRecord;
                //Also open modelspace - we'll be adding our BlockReference to it
                BlockTableRecord ms = bt[BlockTableRecord.ModelSpace].GetObject(OpenMode.ForWrite) as BlockTableRecord;
                //Create new BlockReference, and link it to our block definition
                Point3d point = new Point3d(2.0, 4.0, 6.0);
                using (BlockReference blockRef = new BlockReference(point, blockDef.ObjectId))
                {
                    //Add the block reference to modelspace
                    ms.AppendEntity(blockRef);
                    myT.AddNewlyCreatedDBObject(blockRef, true);
                    //Iterate block definition to find all non-constant
                    // AttributeDefinitions
                    foreach (ObjectId id in blockDef)
                    {
                        DBObject obj = id.GetObject(OpenMode.ForRead);
                        AttributeDefinition attDef = obj as AttributeDefinition;
                        if ((attDef != null) && (!attDef.Constant))
                        {
                            //This is a non-constant AttributeDefinition
                            //Create a new AttributeReference
                            using (AttributeReference attRef = new AttributeReference())
                            {
                                attRef.SetAttributeFromBlock(attDef, blockRef.BlockTransform);
                                attRef.TextString = "Hello World";
                                //Add the AttributeReference to the BlockReference
                                blockRef.AttributeCollection.AppendAttribute(attRef);
                                myT.AddNewlyCreatedDBObject(attRef, true);
                            }
                        }
                    }
                }
                //Our work here is done
                myT.Commit();
            }
        }
    }
}
