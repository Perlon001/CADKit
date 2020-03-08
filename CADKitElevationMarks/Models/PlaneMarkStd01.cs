using CADKit.Utils;
using System.Collections.Generic;
using CADKit.Proxy;
using CADKit.Extensions;
using System;
using CADKit.Contracts;

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
    public class PlaneMarkStd01 : Mark
    {
        public PlaneMarkStd01(PlaneValueProvider _provider) : base("", _provider) { }

        protected override void SetComponentsEntity()
        {
            var en = new List<Entity>();
            
            var txt1 = new AttributeDefinition();
            txt1.SetDatabaseDefaults();
            txt1.TextStyle = CADProxy.Database.Textstyle;
            txt1.HorizontalMode = TextHorizontalMode.TextLeft;
            txt1.VerticalMode = TextVerticalMode.TextVerticalMid;
            txt1.ColorIndex = 7;
            txt1.Height = 2;
            txt1.Position = new Point3d(1.5, 2, 0);
            txt1.Justify = AttachmentPoint.MiddleLeft;
            txt1.AlignmentPoint = new Point3d(1.5, 2, 0);
            txt1.Tag = "Value";
            txt1.Prompt = "Value";
            txt1.TextString = value.Sign + this.value.Value;
            en.Add(txt1);

            var l1 = new Line(new Point3d(-2.5, 0, 0), new Point3d(2.5, 0, 0));
            en.Add(l1);

            var l2 = new Line(new Point3d(0, -2.5, 0), new Point3d(0, 2.5, 0));
            en.Add(l2);

            var c1 = new Circle(new Point3d(0, 0, 0), new Vector3d(0, 0, 1), 1.5);
            en.Add(c1);

            AddHatching(en);
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
                    attRef.TextString = value.Sign + value.Value;
                    blockReference.AttributeCollection.AppendAttribute(attRef);
                }
            }
        }

        private void AddHatching(IList<Entity> en)
        {
            var hatch = new Hatch();
            using (var tr = CADProxy.Database.TransactionManager.StartTransaction())
            {
                var p1 = new Polyline();
                p1.AddVertexAt(0, new Point2d(0, -1.5), 0, 0, 0);
                p1.AddVertexAt(0, new Point2d(0, 1.5), 0, 0, 0);
                p1.AddVertexAt(0, new Point2d(1.5, 0), Bulge.GetBulge(new Point2d(0, 0), new Point2d(0, 1.5), new Point2d(1.5, 0)), 0, 0);
                p1.AddVertexAt(0, new Point2d(-1.5, 0), 0, 0, 0);
                p1.AddVertexAt(0, new Point2d(0, -1.5), Bulge.GetBulge(new Point2d(0, 0), new Point2d(-1.5, 0), new Point2d(0, -1.5)), 0, 0);
                p1.Closed = true;
                BlockTable bt = tr.GetObject(CADProxy.Database.BlockTableId, OpenMode.ForRead) as BlockTable;
                BlockTableRecord btr = tr.GetObject(CADProxy.Database.CurrentSpaceId, OpenMode.ForWrite) as BlockTableRecord;
                var bdId = btr.AppendEntity(p1);
                tr.AddNewlyCreatedDBObject(p1, true);
                ObjectIdCollection ObjIds = new ObjectIdCollection
                {
                    bdId
                };

                hatch.SetDatabaseDefaults();
                hatch.SetHatchPattern(HatchPatternType.PreDefined, "SOLID");
                hatch.Associative = false;
                hatch.AppendLoop((int)HatchLoopTypes.Default, ObjIds);
                hatch.EvaluateHatch(true);
                p1.Erase();
            }
            en.Add(hatch);
        }

        protected override JigMark GetJig()
        {
            var conv = new List<IEntityConverter>
            {
                new AttributeToDBTextConverter()
            };
            return new JigMark(Entities, originPoint, basePoint, conv);
        }

        protected override void BuildComponents()
        {
        }
    }
}
