using System.Collections.Generic;
using CADKit.Utils;
using CADKitElevationMarks.Contracts;
using CADProxy;

#if ZwCAD
using ZwSoft.ZwCAD.DatabaseServices;
using ZwSoft.ZwCAD.EditorInput;
using ZwSoft.ZwCAD.Geometry;
#endif

#if AutoCAD
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
#endif

namespace CADKitElevationMarks.Models
{
    public class ConstructionElevationMarkCADKit : BaseElevationMark
    {
        public ConstructionElevationMarkCADKit(ElevationValue _value, IElevationMarkConfig _config) : base(_value, _config)
        {
        }

        protected override void CreateEntityList()
        {
            throw new System.NotImplementedException();
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
