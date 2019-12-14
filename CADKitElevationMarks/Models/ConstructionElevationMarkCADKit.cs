using System.Collections.Generic;
using System.Linq;
using CADKit.Utils;
using CADKitElevationMarks.Contracts;
using CADProxy;

#if ZwCAD
using ZwSoft.ZwCAD.DatabaseServices;
using ZwSoft.ZwCAD.Geometry;
#endif

#if AutoCAD
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
#endif

namespace CADKitElevationMarks.Models
{
    public class ConstructionElevationMarkCADKit : ElevationMark, IElevationMark
    {
        protected override void CreateEntityList()
        {
            var en = new List<Entity>();

            var tx1 = new DBText();
            tx1.SetDatabaseDefaults();
            tx1.TextStyle = ProxyCAD.Database.Textstyle;
            tx1.HorizontalMode = TextHorizontalMode.TextRight;
            tx1.VerticalMode = TextVerticalMode.TextVerticalMid;
            tx1.ColorIndex = 7;
            tx1.Height = 2;
            tx1.AlignmentPoint = new Point3d(-0.5, 4.5, 0);
            tx1.TextString = this.value.Sign;
            en.Add(tx1);

            var tx2 = new DBText();
            tx2.SetDatabaseDefaults();
            tx2.TextStyle = ProxyCAD.Database.Textstyle;
            tx2.HorizontalMode = TextHorizontalMode.TextLeft;
            tx2.VerticalMode = TextVerticalMode.TextVerticalMid;
            tx2.ColorIndex = 7;
            tx2.Height = 2;
            tx2.AlignmentPoint = new Point3d(0.5, 4.5, 0);
            tx2.TextString = this.value.Value;
            en.Add(tx2);

            var textArea = EntityInfo.GetTextArea(tx2);
            var pl1 = new Polyline();
            pl1.AddVertexAt(0, new Point2d(0, 5.5), 0, 0, 0);
            pl1.AddVertexAt(0, new Point2d(0, 0), 0, 0, 0);
            pl1.AddVertexAt(0, new Point2d(-2, 3), 0, 0, 0);
            pl1.AddVertexAt(0, new Point2d(textArea[1].X - textArea[0].X + 0.5, 3), 0, 0, 0);
            en.Add(pl1);

            AddHatchingArrow(en);
            
            this.entityList = en;
        }

        protected override EntityListJig GetMarkJig(Group _group, Point3d _point)
        {
            return new JigVerticalConstantHorizontalMirrorMark(
                _group.GetAllEntityIds()
                .Select(ent => (Entity)ent
                .GetObject(OpenMode.ForWrite)
                .Clone())
                .ToList(),
                _point);
        }

        private void AddHatchingArrow(IList<Entity> en)
        {
            var hatch = new Hatch();
            using (var tr = ProxyCAD.Database.TransactionManager.StartTransaction())
            {
                var bd = new Polyline();
                bd.AddVertexAt(0, new Point2d(0, 0), 0, 0, 0);
                bd.AddVertexAt(0, new Point2d(-2, 3), 0, 0, 0);
                bd.AddVertexAt(0, new Point2d(0, 3), 0, 0, 0);
                bd.Closed = true;
                BlockTable bt = tr.GetObject(ProxyCAD.Database.BlockTableId, OpenMode.ForRead) as BlockTable;
                BlockTableRecord btr = tr.GetObject(ProxyCAD.Database.CurrentSpaceId, OpenMode.ForWrite) as BlockTableRecord;
                var bdId = btr.AppendEntity(bd);
                tr.AddNewlyCreatedDBObject(bd, true);
                ObjectIdCollection ObjIds = new ObjectIdCollection();
                ObjIds.Add(bdId);

                hatch.SetDatabaseDefaults();
                hatch.SetHatchPattern(HatchPatternType.PreDefined, "SOLID");
                hatch.Associative = false;
                hatch.AppendLoop((int)HatchLoopTypes.Default, ObjIds);
                hatch.EvaluateHatch(true);
                bd.Erase();
            }
            en.Add(hatch);
        }
        //protected override Group DrawEntities(Transaction transaction)
        //{
        //    DBDictionary groupDictionary = (DBDictionary)transaction.GetObject(ProxyCAD.Database.GroupDictionaryId, OpenMode.ForWrite);
        //    Group group = new Group();
        //    groupDictionary.SetAt("*", group);

        //    BlockTableRecord record = Extensions.GetBlockTableRecord(transaction, OpenMode.ForWrite);
        //    TextStyleTableRecord textStyle = ((TextStyleTableRecord)transaction.GetObject(ProxyCAD.Database.Textstyle, OpenMode.ForRead));

        //    foreach (var item in this.texts)
        //    {
        //        item.SetDatabaseDefaults();
        //        item.VerticalMode = TextVerticalMode.TextVerticalMid;
        //        item.ColorIndex = 7;                                    // tez trzeba wczytac z settings !!!
        //        item.TextStyle = ProxyCAD.Database.Textstyle;
        //        item.Oblique = textStyle.ObliquingAngle;
        //        item.WidthFactor = textStyle.XScale;
        //        // item.Height = DI.Container.Resolve<AppSettings>().TextHigh[TextStyles.normal] * scaleFactor;
        //        item.Height = 2;
        //    }

        //    texts[0].HorizontalMode = TextHorizontalMode.TextRight;
        //    texts[1].HorizontalMode = TextHorizontalMode.TextLeft;

        //    var textArea = EntityInfo.GetTextArea(texts[1]);

        //    texts[0].AlignmentPoint = new Point3d(elevationPoint.X - 0.5 * scaleFactor, elevationPoint.Y + 4.5 * scaleFactor, elevationPoint.Z);
        //    texts[1].AlignmentPoint = new Point3d(elevationPoint.X + 0.5 * scaleFactor, elevationPoint.Y + 4.5 * scaleFactor, elevationPoint.Z);

        //    plines.AddVertexAt(0, new Point2d(elevationPoint.X, elevationPoint.Y + 6 * scaleFactor), 0, 0, 0);
        //    plines.AddVertexAt(0, new Point2d(elevationPoint.X, elevationPoint.Y), 0, 0, 0);
        //    plines.AddVertexAt(0, new Point2d(elevationPoint.X - 2 * scaleFactor, elevationPoint.Y + 3 * scaleFactor), 0, 0, 0);
        //    plines.AddVertexAt(0, new Point2d(elevationPoint.X + (textArea[1].X - textArea[0].X + 0.5 * scaleFactor), elevationPoint.Y + 3 * scaleFactor), 0, 0, 0);

        //    Polyline boundary = new Polyline();
        //    boundary.AddVertexAt(0, new Point2d(elevationPoint.X, elevationPoint.Y), 0, 0, 0);
        //    boundary.AddVertexAt(0, new Point2d(elevationPoint.X - 2 * scaleFactor, elevationPoint.Y + 3 * scaleFactor), 0, 0, 0);
        //    boundary.AddVertexAt(0, new Point2d(elevationPoint.X, elevationPoint.Y + 3 * scaleFactor), 0, 0, 0);
        //    boundary.AddVertexAt(0, new Point2d(elevationPoint.X, elevationPoint.Y), 0, 0, 0);
        //    boundary.Closed = true;

        //    if (elevationPoint.Y > directionPoint.Y)
        //    {
        //        plines.TransformBy(Matrix3d.Mirroring(new Line3d(elevationPoint, new Vector3d(1, 0, 0))));
        //        boundary.TransformBy(Matrix3d.Mirroring(new Line3d(elevationPoint, new Vector3d(1, 0, 0))));
        //        texts[0].AlignmentPoint = new Point3d(texts[0].AlignmentPoint.X, elevationPoint.Y - 4.5 * scaleFactor, elevationPoint.Z);
        //        texts[1].AlignmentPoint = new Point3d(texts[1].AlignmentPoint.X, elevationPoint.Y - 4.5 * scaleFactor, elevationPoint.Z);
        //    }

        //    boundary.Store(transaction, record, group, transformMatrix);

        //    ObjectIdCollection collection = new ObjectIdCollection();
        //    collection.Add(boundary.ObjectId);
        //    using (Hatch hatch = new Hatch())
        //    {
        //        hatch.SetDatabaseDefaults();
        //        hatch.Store(transaction, record, group, transformMatrix);
        //        hatch.SetHatchPattern(HatchPatternType.PreDefined, "SOLID");
        //        hatch.Associative = false;
        //        hatch.AppendLoop(HatchLoopTypes.External, collection);
        //        hatch.EvaluateHatch(true);
        //    }
        //    boundary.Erase();

        //    foreach (var item in texts)
        //    {
        //        item.Store(transaction, record, group, transformMatrix);
        //    }

        //    plines.Store(transaction, record, group, transformMatrix);

        //    transaction.AddNewlyCreatedDBObject(group, true);
        //    ProxyCAD.Editor.Regen();

        //    return group;
        //}
    }
}
