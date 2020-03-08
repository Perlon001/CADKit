using CADKit.Contracts;
using CADKit.Extensions;
using CADKit.Proxy;
using CADKit.Utils;
using CADKitElevationMarks.Contracts;
using System;
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
    public class PlaneMarkPNB01025 : Mark
    {
        public PlaneMarkPNB01025(PlaneValueProvider _provider) : base("", _provider)
        {
            Title = "Kota wysokościowa obszaru wg PNB01025";
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

        protected override void BuildComponents()
        {
        }

        protected override void SetComponentsEntity()
        {
            var en = new List<Entity>();

            var att1 = new AttributeDefinition();
            att1.SetDatabaseDefaults();
            att1.TextStyle = CADProxy.Database.Textstyle;
            att1.HorizontalMode = TextHorizontalMode.TextLeft;
            att1.VerticalMode = TextVerticalMode.TextVerticalMid;
            att1.ColorIndex = 7;
            att1.Height = 2;
            att1.Position = new Point3d(2, 1.5, 0);
            att1.Justify = AttachmentPoint.MiddleLeft;
            att1.AlignmentPoint = new Point3d(2, 1.5, 0);
            att1.Tag = "Value";
            att1.Prompt = "Value";
            att1.TextString = value.ToString();
            en.Add(att1);

            var l1 = new Line(new Point3d(-1.5, -1.5, 0), new Point3d(1.5, 1.5, 0));
            en.Add(l1);

            var l2 = new Line(new Point3d(-1.5, 1.5, 0), new Point3d(1.5, -1.5, 0));
            en.Add(l2);

            var textArea = CADProxy.GetTextArea(CADProxy.ToDBText(att1));
            var l3 = new Line(new Point3d(0, 0, 0), new Point3d(textArea[1].X - textArea[0].X + 2, 0, 0));
            en.Add(l3);
        }

        protected override JigMark GetJig()
        {
            var conv = new List<IEntityConverter>
            {
                new AttributeToDBTextConverter()
            };
            return new JigMark(Entities, originPoint, basePoint, conv);
        }
    }
}
