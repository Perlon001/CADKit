using CADKit.Utils;
using CADKitElevationMarks.Contracts;
using CADProxy;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

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
    public class ArchitecturalElevationMarkPNB01025 : ElevationMark
    {
        protected override IEnumerable<Entity> GetEntityList()
        {
            List<Entity> en = new List<Entity>();

            var tx = new DBText();
            tx.SetDatabaseDefaults();
            tx.TextStyle = ProxyCAD.Database.Textstyle;
            tx.HorizontalMode = TextHorizontalMode.TextRight;
            tx.VerticalMode = TextVerticalMode.TextVerticalMid;
            tx.ColorIndex = 7;
            tx.Height = 2;
            tx.AlignmentPoint = new Point3d(1, 4.5, 0);
            tx.TextString = this.value.Sign;
            en.Add(tx);

            tx = new DBText();
            tx.SetDatabaseDefaults();
            tx.TextStyle = ProxyCAD.Database.Textstyle;
            tx.HorizontalMode = TextHorizontalMode.TextLeft;
            tx.VerticalMode = TextVerticalMode.TextVerticalMid;
            tx.ColorIndex = 7;
            tx.Height = 2;
            tx.AlignmentPoint = new Point3d(1.5, 4.5, 0);
            tx.TextString = this.value.Value;
            en.Add(tx);

            var textArea = EntityInfo.GetTextArea(tx);

            var pl = new Polyline();
            pl.AddVertexAt(0, new Point2d(-1, 1), 0, 0, 0);
            pl.AddVertexAt(0, new Point2d(0, 0), 0, 0, 0);
            pl.AddVertexAt(0, new Point2d(1, 1), 0, 0, 0);
            en.Add(pl);
            
            pl = new Polyline();
            pl.AddVertexAt(0, new Point2d(0, 0), 0, 0, 0);
            pl.AddVertexAt(0, new Point2d(0, 3), 0, 0, 0);
            pl.AddVertexAt(0, new Point2d(textArea[1].X - textArea[0].X + 1.5, 3), 0, 0, 0);
            en.Add(pl);
            
            return en;
        }

        protected override MarkJig GetMarkJig(Group _group, Point3d _point)
        {
            return new JigDisplacement(
                _group.GetAllEntityIds()
                .Select(ent => (Entity)ent
                .GetObject(OpenMode.ForWrite)
                .Clone())
                .ToList(),
                _point);
        }

        //public override void Draw()
        //{
        //    //GetElevationPoint();
        //    //PrepareTextFields();
        //    //markEntities = GetEntities();
        //    using (var tr = ProxyCAD.Database.TransactionManager.StartTransaction())
        //    {
        //        try
        //        {
        //            //BlockTableRecord btr = tr.GetObject(db.CurrentSpaceId, OpenMode.ForWrite) as BlockTableRecord;
        //            //foreach (var p in markEntities)
        //            //{
        //            //    btr.AppendEntity(p);
        //            //    tr.AddNewlyCreatedDBObject(p, true);
        //            //}

        //            BlockTable bt = tr.GetObject(db.BlockTableId, OpenMode.ForRead) as BlockTable;
        //            BlockTableRecord btr = tr.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;

        //            var line = new Line(new Point3d(0, 0, 0), new Point3d(100, 100, 0));
        //            btr.AppendEntity(line);
        //            tr.AddNewlyCreatedDBObject(line, true);
        //            tr.Commit();

        //            //jig = new JigVerticalConstantHorizontalMirrorMark(markEntities, elevationPoint);
        //            //PromptResult result = ed.Drag(jig);
        //            //if (result.Status == PromptStatus.OK)
        //            //{
        //            //    foreach (var p in markEntities)
        //            //    {
        //            //        p.TransformBy(jig.Transforms);
        //            //        BlockTableRecord btr = (BlockTableRecord)tr.GetObject(db.CurrentSpaceId, OpenMode.ForWrite);
        //            //        btr.AppendEntity(p);
        //            //        tr.AddNewlyCreatedDBObject(p, true);
        //            //    }

        //            //    tr.Commit();
        //            //}
        //            //else
        //            //{
        //            //    tr.Abort();
        //            //}
        //        }
        //        catch (Exception ex)
        //        {
        //            tr.Abort();
        //            ed.WriteMessage(ex.Message);
        //        }
        //    }
        //}

        //public override IEnumerable<Entity> GetEntity(MarkSidePosition position, Point3d basePoint)
        //{
        //    DBText[] texts = new DBText[] { new DBText(), new DBText() };
        //    Polyline[] plines = new Polyline[] { new Polyline(), new Polyline() };


        //    foreach (var item in texts)
        //    {
        //        item.SetDatabaseDefaults();
        //        item.TextStyle = ProxyCAD.Database.Textstyle;
        //        item.VerticalMode = TextVerticalMode.TextVerticalMid;
        //        item.ColorIndex = 7;
        //        item.Height = 2;
        //    }
        //    texts[0].HorizontalMode = TextHorizontalMode.TextRight;
        //    texts[1].HorizontalMode = TextHorizontalMode.TextLeft;

        //    texts[0].AlignmentPoint = new Point3d(1, 4.5, 0);
        //    texts[0].TextString = "%%p";

        //    texts[1].AlignmentPoint = new Point3d(1.5, 4.5, 0);
        //    texts[1].TextString = "0.000";
        //    var textArea = EntityInfo.GetTextArea(texts[1]);

        //    plines[0].AddVertexAt(0, new Point2d(0, 0), 0, 0, 0);
        //    plines[0].AddVertexAt(0, new Point2d(0, 3), 0, 0, 0);
        //    double posX = textArea[1].X - textArea[0].X + 1.5;
        //    switch (position)
        //    {
        //        case markPosition.left:
        //            posX = -(textArea[1].X - textArea[0].X + 1.5);
        //            break;
        //    }

        //    plines[0].AddVertexAt(0, new Point2d(posX, 3), 0, 0, 0);

        //    plines[1].AddVertexAt(0, new Point2d(-1, 1), 0, 0, 0);
        //    plines[1].AddVertexAt(0, new Point2d(0, 0), 0, 0, 0);
        //    plines[1].AddVertexAt(0, new Point2d(1, 1), 0, 0, 0);

        //    List<Entity> en = new List<Entity>();
        //    foreach (var item in texts)
        //    {
        //        en.Add(item);
        //    }
        //    foreach (var item in plines)
        //    {
        //        en.Add(item);
        //    }

        //    return en;
        //}


        //public void _Draw()
        //{
        //    GetElevationPoint();
        //    PrepareTextFields();
        //    using (var tr = ProxyCAD.Database.TransactionManager.StartTransaction())
        //    {
        //        try
        //        {
        //            var group = DrawEntities(tr);
        //            markEntities = group
        //                .GetAllEntityIds()
        //                .Select(ent => (Entity)ent.GetObject(OpenMode.ForWrite).Clone())
        //                .ToList();

        //            jig = new JigVerticalConstantHorizontalMirrorMark(markEntities, elevationPoint.TransformBy((ed.CurrentUserCoordinateSystem)));
        //            PromptResult result = ed.Drag(jig);
        //            if (result.Status == PromptStatus.OK)
        //            {
        //                group.SetVisibility(true);
        //                foreach (var e in group.GetAllEntityIds())
        //                {
        //                    e.GetObject(OpenMode.ForWrite).Erase(true);
        //                }
        //                foreach (var p in markEntities)
        //                {
        //                    p.TransformBy(jig.Transforms);
        //                    BlockTableRecord btr = (BlockTableRecord)tr.GetObject(db.CurrentSpaceId, OpenMode.ForWrite);
        //                    btr.AppendEntity(p);
        //                    tr.AddNewlyCreatedDBObject(p, true);
        //                    group.Append(p.ObjectId);
        //                }

        //                tr.Commit();
        //            }
        //            else
        //            {
        //                tr.Abort();
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            tr.Abort();
        //            ed.WriteMessage(ex.Message);
        //        }
        //    }
        //}
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

        //    texts[0].AlignmentPoint = new Point3d(elevationPoint.X + 1 * scaleFactor, elevationPoint.Y + 4.5 * scaleFactor, elevationPoint.Z);
        //    texts[1].AlignmentPoint = new Point3d(elevationPoint.X + 1.5 * scaleFactor, elevationPoint.Y + 4.5 * scaleFactor, elevationPoint.Z);

        //    plines[0].AddVertexAt(0, new Point2d(elevationPoint.X, elevationPoint.Y), 0, 0, 0);
        //    plines[0].AddVertexAt(0, new Point2d(elevationPoint.X, elevationPoint.Y + 3 * scaleFactor), 0, 0, 0);
        //    plines[0].AddVertexAt(0, new Point2d(elevationPoint.X + (textArea[1].X - textArea[0].X + 1.5 * scaleFactor), elevationPoint.Y + 3 * scaleFactor), 0, 0, 0);

        //    plines[1].AddVertexAt(0, new Point2d(elevationPoint.X - 1 * scaleFactor, elevationPoint.Y + 1 * scaleFactor), 0, 0, 0);
        //    plines[1].AddVertexAt(0, new Point2d(elevationPoint.X, elevationPoint.Y), 0, 0, 0);
        //    plines[1].AddVertexAt(0, new Point2d(elevationPoint.X + 1 * scaleFactor, elevationPoint.Y + 1 * scaleFactor), 0, 0, 0);

        //    Polyline boundary = new Polyline();
        //    boundary.AddVertexAt(0, new Point2d(elevationPoint.X, elevationPoint.Y), 0, 0, 0);
        //    boundary.AddVertexAt(0, new Point2d(elevationPoint.X + 1 * scaleFactor, elevationPoint.Y + 1 * scaleFactor), 0, 0, 0);
        //    boundary.AddVertexAt(0, new Point2d(elevationPoint.X, elevationPoint.Y + 1 * scaleFactor), 0, 0, 0);
        //    boundary.Closed = true;

        //    if (Math.Round(Math.Abs(elevationPoint.Y) * GetElevationFactor(), 3) == 0)
        //    {
        //        IsZero = true;
        //        plines[1].Closed = true;
        //    }

        //if (elevationPoint.Y >= directionPoint.Y)
        //{
        //    plines[0].TransformBy(Matrix3d.Mirroring(new Line3d(elevationPoint, new Vector3d(1, 0, 0))));
        //    plines[1].TransformBy(Matrix3d.Mirroring(new Line3d(elevationPoint, new Vector3d(1, 0, 0))));
        //    boundary.TransformBy(Matrix3d.Mirroring(new Line3d(elevationPoint, new Vector3d(1, 0, 0))));
        //    texts[0].AlignmentPoint = new Point3d(texts[0].AlignmentPoint.X, elevationPoint.Y - 4.5 * scaleFactor, elevationPoint.Z);
        //    texts[1].AlignmentPoint = new Point3d(texts[1].AlignmentPoint.X, elevationPoint.Y - 4.5 * scaleFactor, elevationPoint.Z);
        //}

        //if (elevationPoint.X >= directionPoint.X)
        //{
        //    plines[0].TransformBy(Matrix3d.Mirroring(new Line3d(elevationPoint, new Vector3d(0, 1, 0))));
        //    plines[1].TransformBy(Matrix3d.Mirroring(new Line3d(elevationPoint, new Vector3d(0, 1, 0))));
        //    texts[0].AlignmentPoint = new Point3d(elevationPoint.X + 1 * scaleFactor - (textArea[1].X - textArea[0].X + 1.5 * scaleFactor), texts[0].AlignmentPoint.Y, elevationPoint.Z);
        //    texts[1].AlignmentPoint = new Point3d(elevationPoint.X + 1.5 * scaleFactor - (textArea[1].X - textArea[0].X + 1.5 * scaleFactor), texts[1].AlignmentPoint.Y, elevationPoint.Z);
        //}

        //    if (IsZero)
        //    {
        //        boundary.Store(transaction, record, group, transformMatrix);
        //        ObjectIdCollection collection = new ObjectIdCollection();
        //        collection.Add(boundary.ObjectId);
        //        using (Hatch hatch = new Hatch())
        //        {
        //            hatch.SetDatabaseDefaults();
        //            hatch.Store(transaction, record, group, transformMatrix);
        //            hatch.SetHatchPattern(HatchPatternType.PreDefined, "SOLID");
        //            hatch.Associative = false;
        //            hatch.AppendLoop(HatchLoopTypes.Default, collection);
        //            hatch.EvaluateHatch(true);
        //        }
        //        boundary.Erase();
        //    }

        //    foreach (var item in texts)
        //    {
        //        item.Store(transaction, record, group, transformMatrix);
        //    }

        //    plines[0].Store(transaction, record, group, transformMatrix);
        //    plines[1].Store(transaction, record, group, transformMatrix);

        //    transaction.AddNewlyCreatedDBObject(group, true);
        //    ed.Regen();

        //    return group;
        //}

        //protected override IEnumerable<Entity> GetEntities()
        //{
        //    foreach (var item in this.texts)
        //    {
        //        item.SetDatabaseDefaults();
        //        item.TextStyle = ProxyCAD.Database.Textstyle;
        //        item.VerticalMode = TextVerticalMode.TextVerticalMid;
        //        item.ColorIndex = 7;         
        //        item.Height = 2;
        //    }
        //    texts[0].HorizontalMode = TextHorizontalMode.TextRight;
        //    texts[1].HorizontalMode = TextHorizontalMode.TextLeft;

        //    texts[0].AlignmentPoint = new Point3d(1, 4.5, 0);
        //    texts[1].AlignmentPoint = new Point3d(1.5, 4.5, 0);
        //    var textArea = EntityInfo.GetTextArea(texts[1]);

        //    plines[0].AddVertexAt(0, new Point2d(0, 0), 0, 0, 0);
        //    plines[0].AddVertexAt(0, new Point2d(0, 3), 0, 0, 0);
        //    plines[0].AddVertexAt(0, new Point2d((textArea[1].X - textArea[0].X + 1.5), 3), 0, 0, 0);

        //    plines[1].AddVertexAt(0, new Point2d(- 1, 1), 0, 0, 0);
        //    plines[1].AddVertexAt(0, new Point2d(0, 0), 0, 0, 0);
        //    plines[1].AddVertexAt(0, new Point2d(1, 1), 0, 0, 0);

        //    List<Entity> en = new List<Entity>();
        //    foreach (var item in texts)
        //    {
        //        en.Add(item);
        //    }
        //    foreach (var item in plines)
        //    {
        //        en.Add(item);
        //    }

        //    return en;
        //}
    }
}

    