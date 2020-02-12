using CADKit.Proxy;
using CADKitElevationMarks.Contracts;
using System.Collections.Generic;

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
    public class MarkPNB01025 : IMark
    {
        private readonly ElevationValue value;

        public MarkPNB01025(IElevationValueProvider _provider)
        {
            _provider.PrepareValue();
            value = _provider.ElevationValue;
            BasePoint = _provider.BasePoint;
        }

        public Point3d BasePoint { get; }

        public virtual IEnumerable<Entity> GetEntities()
        {
            var en = new List<Entity>();
            var txt1 = new AttributeDefinition();
            var pl1 = new Polyline();
            var pl2 = new Polyline();

            txt1.SetDatabaseDefaults();
            txt1.TextStyle = CADProxy.Database.Textstyle;
            txt1.HorizontalMode = TextHorizontalMode.TextLeft;
            txt1.VerticalMode = TextVerticalMode.TextVerticalMid;
            txt1.ColorIndex = 7;
            txt1.Height = 2;
            txt1.Position = new Point3d(0, 4.5, 0);
            txt1.Justify = AttachmentPoint.MiddleLeft;
            txt1.AlignmentPoint = new Point3d(0, 4.5, 0);
            txt1.Tag = "Value";
            txt1.Prompt = "Value";
            txt1.TextString = this.value.ToString();
            en.Add(txt1);

            pl1.AddVertexAt(0, new Point2d(-1.5, 1.5), 0, 0, 0);
            pl1.AddVertexAt(0, new Point2d(0, 0), 0, 0, 0);
            pl1.AddVertexAt(0, new Point2d(1.5, 1.5), 0, 0, 0);
            if (value.Value == "0,000")
            {
                pl1.Closed = true;
                AddHatchingArrow(en);
            }
            en.Add(pl1);

            var textArea = CADProxy.GetTextArea(CADProxy.ToDBText(txt1));
            pl2.AddVertexAt(0, new Point2d(0, 0), 0, 0, 0);
            pl2.AddVertexAt(0, new Point2d(0, 3), 0, 0, 0);
            pl2.AddVertexAt(0, new Point2d(textArea[1].X - textArea[0].X, 3), 0, 0, 0);
            en.Add(pl2);

            return en;
        }

        private void AddHatchingArrow(IList<Entity> entity)
        {
            using (var transaction = CADProxy.Database.TransactionManager.StartTransaction())
            {
                var polyline = new Polyline();
                polyline.AddVertexAt(0, new Point2d(0, 0), 0, 0, 0);
                polyline.AddVertexAt(0, new Point2d(1.5, 1.5), 0, 0, 0);
                polyline.AddVertexAt(0, new Point2d(0, 1.5), 0, 0, 0);
                polyline.Closed = true;

                var blockTableRecord = transaction.GetObject(CADProxy.Database.CurrentSpaceId, OpenMode.ForWrite) as BlockTableRecord;
                var objIds = new ObjectIdCollection()
                {
                    blockTableRecord.AppendEntity(polyline)
                };
                transaction.AddNewlyCreatedDBObject(polyline, true);

                var hatch = new Hatch();
                hatch.SetDatabaseDefaults();
                hatch.SetHatchPattern(HatchPatternType.PreDefined, "SOLID");
                hatch.Associative = false;
                hatch.AppendLoop((int)HatchLoopTypes.Default, objIds);
                hatch.EvaluateHatch(true);

                polyline.Erase();

                entity.Add(hatch);
            }
        }
    }
}
