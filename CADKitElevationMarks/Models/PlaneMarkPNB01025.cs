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
    public class PlaneMarkPNB01025 : Mark
    {
        public PlaneMarkPNB01025(IPlaneValueProvider _provider) : base(_provider) { }

        public override IEnumerable<Entity> GetEntities()
        {
            var en = new List<Entity>();

            var txt1 = new AttributeDefinition();
            txt1.SetDatabaseDefaults();
            txt1.TextStyle = CADProxy.Database.Textstyle;
            txt1.HorizontalMode = TextHorizontalMode.TextLeft;
            txt1.VerticalMode = TextVerticalMode.TextVerticalMid;
            txt1.ColorIndex = 7;
            txt1.Height = 2;
            txt1.Position = new Point3d(2, 1.5, 0);
            txt1.Justify = AttachmentPoint.MiddleLeft;
            txt1.AlignmentPoint = new Point3d(2, 1.5, 0);
            txt1.Tag = "Value";
            txt1.Prompt = "Value";
            txt1.TextString = value.ToString();
            en.Add(txt1);

            var l1 = new Line(new Point3d(-1.5, -1.5, 0), new Point3d(1.5, 1.5, 0));
            en.Add(l1);

            var l2 = new Line(new Point3d(-1.5, 1.5, 0), new Point3d(1.5, -1.5, 0));
            en.Add(l2);

            var textArea = CADProxy.GetTextArea(CADProxy.ToDBText(txt1));
            var l3 = new Line(new Point3d(0, 0, 0), new Point3d(textArea[1].X - textArea[0].X + 2, 0, 0));
            en.Add(l3);

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
    }
}
