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
    public class ConstructionElevationMarkCADKit : BaseElevationMark
    {
        private Polyline plines = new Polyline();

        public ConstructionElevationMarkCADKit(IElevationMarkConfig config) : base(config)
        {
        }

        protected override void Draw(Transaction transaction)
        {
            DBDictionary groupDictionary = (DBDictionary)transaction.GetObject(ProxyCAD.Database.GroupDictionaryId, OpenMode.ForWrite);
            Group group = new Group();
            groupDictionary.SetAt("*", group);

            BlockTableRecord record = Extensions.GetBlockTableRecord(transaction, OpenMode.ForWrite);
            TextStyleTableRecord textStyle = ((TextStyleTableRecord)transaction.GetObject(ProxyCAD.Database.Textstyle, OpenMode.ForRead));

            foreach (var item in this.texts)
            {
                item.SetDatabaseDefaults();
                item.VerticalMode = TextVerticalMode.TextVerticalMid;
                item.ColorIndex = 7;                                    // tez trzeba wczytac z settings !!!
                item.TextStyle = ProxyCAD.Database.Textstyle;
                item.Oblique = textStyle.ObliquingAngle;
                item.WidthFactor = textStyle.XScale;
                // item.Height = DI.Container.Resolve<AppSettings>().TextHigh[TextStyles.normal] * scaleFactor;
                item.Height = 2;
            }

            texts[0].HorizontalMode = TextHorizontalMode.TextRight;
            texts[1].HorizontalMode = TextHorizontalMode.TextLeft;

            var textArea = EntityInfo.GetTextArea(texts[1]);

            texts[0].AlignmentPoint = new Point3d(point.X - 0.5 * scaleFactor, point.Y + 4.5 * scaleFactor, point.Z);
            texts[1].AlignmentPoint = new Point3d(point.X + 0.5 * scaleFactor, point.Y + 4.5 * scaleFactor, point.Z);

            plines.AddVertexAt(0, new Point2d(point.X, point.Y + 6 * scaleFactor), 0, 0, 0);
            plines.AddVertexAt(0, new Point2d(point.X, point.Y), 0, 0, 0);
            plines.AddVertexAt(0, new Point2d(point.X - 2 * scaleFactor, point.Y + 3 * scaleFactor), 0, 0, 0);
            plines.AddVertexAt(0, new Point2d(point.X + (textArea[1].X - textArea[0].X + 0.5 * scaleFactor), point.Y + 3 * scaleFactor), 0, 0, 0);

            Polyline boundary = new Polyline();
            boundary.AddVertexAt(0, new Point2d(point.X, point.Y), 0, 0, 0);
            boundary.AddVertexAt(0, new Point2d(point.X - 2 * scaleFactor, point.Y + 3 * scaleFactor), 0, 0, 0);
            boundary.AddVertexAt(0, new Point2d(point.X, point.Y + 3 * scaleFactor), 0, 0, 0);
            boundary.AddVertexAt(0, new Point2d(point.X, point.Y), 0, 0, 0);
            boundary.Closed = true;

            if (point.Y > placePoint.Y)
            {
                plines.TransformBy(Matrix3d.Mirroring(new Line3d(point, new Vector3d(1, 0, 0))));
                boundary.TransformBy(Matrix3d.Mirroring(new Line3d(point, new Vector3d(1, 0, 0))));
                texts[0].AlignmentPoint = new Point3d(texts[0].AlignmentPoint.X, point.Y - 4.5 * scaleFactor, point.Z);
                texts[1].AlignmentPoint = new Point3d(texts[1].AlignmentPoint.X, point.Y - 4.5 * scaleFactor, point.Z);
            }

            boundary.Store(transaction, record, group, transformMatrix);

            ObjectIdCollection collection = new ObjectIdCollection();
            collection.Add(boundary.ObjectId);
            using (Hatch hatch = new Hatch())
            {
                hatch.SetDatabaseDefaults();
                hatch.Store(transaction, record, group, transformMatrix);
                hatch.SetHatchPattern(HatchPatternType.PreDefined, "SOLID");
                hatch.Associative = false;
                hatch.AppendLoop(HatchLoopTypes.External, collection);
                hatch.EvaluateHatch(true);
            }
            boundary.Erase();

            foreach (var item in texts)
            {
                item.Store(transaction, record, group, transformMatrix);
            }

            plines.Store(transaction, record, group, transformMatrix);

            transaction.AddNewlyCreatedDBObject(group, true);
            ProxyCAD.Editor.Regen();
        }
    }
}
