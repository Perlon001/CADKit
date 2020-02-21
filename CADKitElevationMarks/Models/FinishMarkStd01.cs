using System;
using System.Collections.Generic;
using CADKit.Contracts;
using CADKit.Extensions;
using CADKit.Proxy;
using CADKit.Utils;

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
    public class FinishMarkStd01 : Mark
    {
        public FinishMarkStd01(ElevationValueProvider _provider) : base("", _provider) { }

        protected override IEnumerable<Entity> GetEntities()
        {
            var en = new List<Entity>();

            var att1 = new AttributeDefinition();
            att1.SetDatabaseDefaults();
            att1.TextStyle = CADProxy.Database.Textstyle;
            att1.HorizontalMode = TextHorizontalMode.TextRight;
            att1.VerticalMode = TextVerticalMode.TextVerticalMid;
            att1.ColorIndex = 7;
            att1.Height = 2;
            att1.Position = new Point3d(-0.5, 4.5, 0);
            att1.Justify = AttachmentPoint.MiddleRight;
            att1.AlignmentPoint = new Point3d(-0.5, 4.5, 0);
            att1.Tag = "Sign";
            att1.Prompt = "Sign";
            att1.TextString = value.Sign;
            en.Add(att1);

            var att2 = new AttributeDefinition();
            att2.SetDatabaseDefaults();
            att2.TextStyle = CADProxy.Database.Textstyle;
            att2.HorizontalMode = TextHorizontalMode.TextLeft;
            att2.VerticalMode = TextVerticalMode.TextVerticalMid;
            att2.ColorIndex = 7;
            att2.Height = 2;
            att2.Position = new Point3d(0.5, 4.5, 0);
            att2.Justify = AttachmentPoint.MiddleLeft;
            att2.AlignmentPoint = new Point3d(0.5, 4.5, 0);
            att2.Tag = "Value";
            att2.Prompt = "Value";
            att2.TextString = value.Value;
            en.Add(att2);

            var textArea = CADProxy.GetTextArea(CADProxy.ToDBText(att2));
            var pl1 = new Polyline();
            pl1.AddVertexAt(0, new Point2d(0, 5.5), 0, 0, 0);
            pl1.AddVertexAt(0, new Point2d(0, 0), 0, 0, 0);
            pl1.AddVertexAt(0, new Point2d(-2, 3), 0, 0, 0);
            pl1.AddVertexAt(0, new Point2d(textArea[1].X - textArea[0].X + 0.5, 3), 0, 0, 0);
            en.Add(pl1);

            return en;
        }

        public override void SetAttributeValue(BlockReference blockReference)
        {
            using (var blockTableRecord = blockReference.BlockTableRecord.GetObject(OpenMode.ForRead) as BlockTableRecord)
            {
                var attDef = blockTableRecord.GetAttribDefinition("Sign");
                if (!attDef.Constant)
                {
                    var attRef = new AttributeReference();
                    attRef.SetAttributeFromBlock(attDef, blockReference.BlockTransform);
                    attRef.TextString = value.Sign;
                    blockReference.AttributeCollection.AppendAttribute(attRef);
                }
                attDef = blockTableRecord.GetAttribDefinition("Value");
                if (!attDef.Constant)
                {
                    var attRef = new AttributeReference();
                    attRef.SetAttributeFromBlock(attDef, blockReference.BlockTransform);
                    attRef.TextString = value.Value;
                    blockReference.AttributeCollection.AppendAttribute(attRef);
                }
            }
        }

        protected override JigMark GetJig()
        {
            var conv = new List<IEntityConverter>
            {
                new AttributeToDBTextConverter()
            };
            return new JigVerticalConstantHorizontalMirrorMark(entities, originPoint, basePoint, conv);
        }
    }
}

