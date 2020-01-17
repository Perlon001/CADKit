using CADKit;
using CADKit.Models;
using CADKit.Utils;
using CADProxy;
using System;
using System.Linq;
using System.Collections.Generic;
using CADProxy.Extensions;

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
    public class ElevationMarkPNB01025 : ElevationMark
    {
        public ElevationMarkPNB01025() : base() { }

        public override DrawingStandards DrawingStandard { get { return DrawingStandards.PNB01025; } }

        public override MarkTypes MarkType { get { return MarkTypes.universal; } }

        public override void CreateEntityList()
        {
            var en = new List<Entity>();
            var txt1 = new AttributeDefinition();
            var pl1 = new Polyline();
            var pl2 = new Polyline();

            txt1.SetDatabaseDefaults();
            txt1.TextStyle = ProxyCAD.Database.Textstyle;
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
            if (Math.Round(Math.Abs(this.basePoint.Value.Y) * AppSettings.Instance.ScaleFactor, 3) == 0)
            {
                pl1.Closed = true;
                AddHatchingArrow(en);
                index = "Zero";
            }
            en.Add(pl1);

            var textArea = ProxyCAD.GetTextArea(ProxyCAD.ToDBText(txt1));
            pl2.AddVertexAt(0, new Point2d(0, 0), 0, 0, 0);
            pl2.AddVertexAt(0, new Point2d(0, 3), 0, 0, 0);
            pl2.AddVertexAt(0, new Point2d(textArea[1].X - textArea[0].X, 3), 0, 0, 0);
            en.Add(pl2);

            this.entityList = en;
        }

        protected override JigMark GetMarkJig()
        {
            return new JigVerticalConstantVerticalAndHorizontalMirrorMark(entityList, basePoint.Value, new AttributeToDBTextConverter());
        }

        protected override void SetAttributeValue(BlockReference blockReference)
        {
            using (var blockTableRecord = blockReference.BlockTableRecord.GetObject(OpenMode.ForRead) as BlockTableRecord)
            {
                var attDef = blockTableRecord.GetAttribDefinition("Value");
                if (!attDef.Constant)
                {
                    var attRef = new AttributeReference();
                    attRef.SetAttributeFromBlock(attDef, blockReference.BlockTransform);
                    attRef.TextString = value.Sign + value.Value;
                    blockReference.AttributeCollection.AppendAttribute(attRef);
                }
            }
        }

        private void AddHatchingArrow(IList<Entity> entity)
        {
            using (var transaction = ProxyCAD.Database.TransactionManager.StartTransaction())
            {
                var polyline = new Polyline();
                polyline.AddVertexAt(0, new Point2d(0, 0), 0, 0, 0);
                polyline.AddVertexAt(0, new Point2d(1.5, 1.5), 0, 0, 0);
                polyline.AddVertexAt(0, new Point2d(0, 1.5), 0, 0, 0);
                polyline.Closed = true;

                var blockTableRecord = transaction.GetObject(ProxyCAD.Database.CurrentSpaceId, OpenMode.ForWrite) as BlockTableRecord;
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

