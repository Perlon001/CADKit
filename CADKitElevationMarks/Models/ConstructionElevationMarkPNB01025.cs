using CADKit.Utils;
using CADKitElevationMarks.Contracts;
using CADKitElevationMarks.Modelsm;
using System;

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
    public class ConstructionElevationMarkPNB01025 : ElevationMark, IElevationMark
    {
        protected override void CreateEntityList()
        {
            throw new NotImplementedException();
        }

        protected override EntityListJig GetMarkJig(Group group, Point3d point)
        {
            throw new NotImplementedException();
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

        //    if (elevationPoint.Y > directionPoint.Y)
        //    {
        //        plines[0].TransformBy(Matrix3d.Mirroring(new Line3d(elevationPoint, new Vector3d(1, 0, 0))));
        //        plines[1].TransformBy(Matrix3d.Mirroring(new Line3d(elevationPoint, new Vector3d(1, 0, 0))));
        //        boundary.TransformBy(Matrix3d.Mirroring(new Line3d(elevationPoint, new Vector3d(1, 0, 0))));
        //        texts[0].AlignmentPoint = new Point3d(texts[0].AlignmentPoint.X, elevationPoint.Y - 4.5 * scaleFactor, elevationPoint.Z);
        //        texts[1].AlignmentPoint = new Point3d(texts[1].AlignmentPoint.X, elevationPoint.Y - 4.5 * scaleFactor, elevationPoint.Z);
        //    }

        //    if (elevationPoint.X > directionPoint.X)
        //    {
        //        plines[0].TransformBy(Matrix3d.Mirroring(new Line3d(elevationPoint, new Vector3d(0, 1, 0))));
        //        plines[1].TransformBy(Matrix3d.Mirroring(new Line3d(elevationPoint, new Vector3d(0, 1, 0))));
        //        texts[0].AlignmentPoint = new Point3d(elevationPoint.X + 1 * scaleFactor - (textArea[1].X - textArea[0].X + 1.5 * scaleFactor), texts[0].AlignmentPoint.Y, elevationPoint.Z);
        //        texts[1].AlignmentPoint = new Point3d(elevationPoint.X + 1.5 * scaleFactor - (textArea[1].X - textArea[0].X + 1.5 * scaleFactor), texts[1].AlignmentPoint.Y, elevationPoint.Z);
        //    }

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
        //    ProxyCAD.Document.Editor.Regen();

        //    return group;
        //}
    }
}
