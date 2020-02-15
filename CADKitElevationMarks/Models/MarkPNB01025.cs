using CADKit.Extensions;
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
    public class MarkPNB01025 : Mark
    {
        public MarkPNB01025(ElevationValueProvider _provider) : base(_provider) { }

        public override IEnumerable<Entity> GetEntities()
        {
            var en = new List<Entity>();
            var att1 = new AttributeDefinition();
            att1.SetDatabaseDefaults();
            att1.TextStyle = CADProxy.Database.Textstyle;
            att1.HorizontalMode = TextHorizontalMode.TextLeft;
            att1.VerticalMode = TextVerticalMode.TextVerticalMid;
            att1.ColorIndex = 7;
            att1.Height = 2;
            att1.Position = new Point3d(0, 4.5, 0);
            att1.Justify = AttachmentPoint.MiddleLeft;
            att1.AlignmentPoint = new Point3d(0, 4.5, 0);
            att1.Tag = "Value";
            att1.Prompt = "Value";
            att1.TextString = Value.ToString();
            en.Add(att1);

            var pl1 = new Polyline();
            pl1.AddVertexAt(0, new Point2d(-1.5, 1.5), 0, 0, 0);
            pl1.AddVertexAt(0, new Point2d(0, 0), 0, 0, 0);
            pl1.AddVertexAt(0, new Point2d(1.5, 1.5), 0, 0, 0);
            if (Value.Value == "0,000")
            {
                pl1.Closed = true;
                AddHatchingArrow(en);
                Index = "zero";
            }
            en.Add(pl1);

            var textArea = CADProxy.GetTextArea(CADProxy.ToDBText(att1));
            var pl2 = new Polyline();
            pl2.AddVertexAt(0, new Point2d(0, 0), 0, 0, 0);
            pl2.AddVertexAt(0, new Point2d(0, 3), 0, 0, 0);
            pl2.AddVertexAt(0, new Point2d(textArea[1].X - textArea[0].X, 3), 0, 0, 0);
            en.Add(pl2);

            return en;
        }

        public override void SetAttributeValue(BlockReference blockReference)
        {
            using (var blockTableRecord = blockReference.BlockTableRecord.GetObject(OpenMode.ForRead) as BlockTableRecord)
            {
                var attDef = blockTableRecord.GetAttribDefinition("Value");
                if (!attDef.Constant)
                {
                    var attRef = new AttributeReference();
                    attRef.SetAttributeFromBlock(attDef, blockReference.BlockTransform);
                    attRef.TextString = Value.Sign + Value.Value;
                    blockReference.AttributeCollection.AppendAttribute(attRef);
                }
            }
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
