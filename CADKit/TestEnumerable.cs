using CADKit.Extensions;
using CADProxy;
using CADProxy.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZwSoft.ZwCAD.ApplicationServices;
using ZwSoft.ZwCAD.DatabaseServices;
using ZwSoft.ZwCAD.Geometry;
using ZwSoft.ZwCAD.Runtime;

namespace CADKitTest
{
    public static class GroupExtension
    {
        public class TestEnumerable
        {

            [CommandMethod("TEST1")]
            public static void TestEnumerableCommand()
            {
                var entities = GetEntity();
                var entities1 = entities.ToGroup().ToEnumerable();
                // var group1 = entities.ToGroup();
                // var block1 = entities.ToBlock("bbb", new Point3d(0, 0, 0));
                // var block2 = entities.Clone().ToGroup().GetAllEntityIds().Select(x => x.GetObject(OpenMode.ForRead) as Entity).ToBlock("bbb", new Point3d(0, 0, 0));
                // var block3 = entities.ToBlock("bbb", new Point3d(0, 0, 0));
                //ProxyCAD.Editor.WriteMessage(block1.Name);
                //InsertBlock(new Point3d(20, 20, 0), block1);
                //ProxyCAD.Editor.WriteMessage(block2.Name);
                //var refBlock1 = InsertBlock(new Point3d(30, 30, 0), block2);
                InsertBlock("bbb");
                ProxyCAD.ZoomExtens();
            }

            public static void InsertBlock(string _name)
            {
                Document doc = Application.DocumentManager.MdiActiveDocument;
                using(var tr = doc.TransactionManager.StartTransaction())
                {
                    var bt = tr.GetObject(doc.Database.BlockTableId, OpenMode.ForRead) as BlockTable;
                    if (bt.Has(_name))
                    {
                        var space = tr.GetObject(doc.Database.CurrentSpaceId, OpenMode.ForWrite) as BlockTableRecord;
                        var bd = bt[_name].GetObject(OpenMode.ForRead) as BlockTableRecord;
                        using (var br = new BlockReference(new Point3d(20, 20, 0), bd.ObjectId))
                        {
                            space.AppendEntity(br);
                            tr.AddNewlyCreatedDBObject(br, true);
                            foreach(var id in bd)
                            {
                                var ad = id.GetObject(OpenMode.ForRead) as AttributeDefinition;
                                if((ad != null) && (!ad.Constant))
                                {
                                    using(var ar = new AttributeReference())
                                    {
                                        ar.SetAttributeFromBlock(ad, br.BlockTransform);
                                        ar.TextString = "Some text";
                                        br.AttributeCollection.AppendAttribute(ar);
                                        tr.AddNewlyCreatedDBObject(ar, true);
                                    }
                                }
                            }
                        }
                    }
                    tr.Commit();
                }
            }
            
            public static BlockReference InsertBlock(Point3d _insertionPoint, BlockTableRecord _block)
            {
                var doc = Application.DocumentManager.MdiActiveDocument;
                using(var tr = doc.TransactionManager.StartTransaction())
                {
                    var bt = tr.GetObject(doc.Database.BlockTableId, OpenMode.ForRead) as BlockTable;
                    if (bt.Has(_block.Name))
                    {
                        var br = new BlockReference(_insertionPoint, bt[_block.Name].GetObject(OpenMode.ForRead).ObjectId);
                        var space = tr.GetObject(doc.Database.CurrentSpaceId, OpenMode.ForWrite) as BlockTableRecord;
                        space.AppendEntity(br);
                        tr.AddNewlyCreatedDBObject(br, true);
                        tr.Commit();
                        return br;
                    }
                    else
                    {
                        throw new EntryPointNotFoundException("Brak definicji bloku " + _block.Name);
                    }
                }
            }

            public static IEnumerable<Entity> GetEntity()
            {
                var en = new List<Entity>();
                var tx1 = new AttributeDefinition();
                var pl1 = new Polyline();
                var pl2 = new Polyline();

                tx1.SetDatabaseDefaults();
                tx1.HorizontalMode = TextHorizontalMode.TextLeft;
                tx1.VerticalMode = TextVerticalMode.TextVerticalMid;
                tx1.ColorIndex = 7;
                tx1.Height = 2;
                tx1.Position = new Point3d(0, 4.5, 0);
                tx1.AlignmentPoint = new Point3d(0, 4.5, 0);
                tx1.Justify = AttachmentPoint.MiddleLeft;
                tx1.Tag = "Value";
                tx1.Prompt = "Value";
                tx1.TextString = "%%p2,450";
                en.Add(tx1);

                pl1.SetDatabaseDefaults();
                pl1.AddVertexAt(0, new Point2d(-1.5, 1.5), 0, 0, 0);
                pl1.AddVertexAt(0, new Point2d(0, 0), 0, 0, 0);
                pl1.AddVertexAt(0, new Point2d(1.5, 1.5), 0, 0, 0);
                en.Add(pl1);

                pl2.SetDatabaseDefaults();
                pl2.AddVertexAt(0, new Point2d(0, 0), 0, 0, 0);
                pl2.AddVertexAt(0, new Point2d(0, 3), 0, 0, 0);
                pl2.AddVertexAt(0, new Point2d(10, 3), 0, 0, 0);
                en.Add(pl2);

                return en as IEnumerable<Entity>;
            }

        }
    }
}
