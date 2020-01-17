using System.Linq;
using System.Collections.Generic;

using CADProxy;
using CADKit.Utils;
using CADKitElevationMarks.Contracts;
using CADKit.Models;
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
    public class FinishElevationMarkCADKit : ElevationMark, IElevationMark
    {
        public override DrawingStandards DrawingStandard { get { return DrawingStandards.CADKit; } }

        public override MarkTypes MarkType { get { return MarkTypes.finish; } }

        public override void CreateEntityList()
        {
            var en = new List<Entity>();

            var txt1 = new AttributeDefinition();
            txt1.SetDatabaseDefaults();
            txt1.TextStyle = ProxyCAD.Database.Textstyle;
            txt1.HorizontalMode = TextHorizontalMode.TextRight;
            txt1.VerticalMode = TextVerticalMode.TextVerticalMid;
            txt1.ColorIndex = 7;
            txt1.Height = 2;
            txt1.Position = new Point3d(-0.5, 4.5, 0);
            txt1.Justify = AttachmentPoint.MiddleRight;
            txt1.AlignmentPoint = new Point3d(-0.5, 4.5, 0);
            txt1.Tag = "Sign";
            txt1.Prompt = "Sign";
            txt1.TextString = value.Sign;
            en.Add(txt1);
            
            var txt2 = new AttributeDefinition();
            txt2.SetDatabaseDefaults();
            txt2.TextStyle = ProxyCAD.Database.Textstyle;
            txt2.HorizontalMode = TextHorizontalMode.TextLeft;
            txt2.VerticalMode = TextVerticalMode.TextVerticalMid;
            txt2.ColorIndex = 7;
            txt2.Height = 2;
            txt2.Position = new Point3d(0.5, 4.5, 0);
            txt2.Justify = AttachmentPoint.MiddleRight;
            txt2.AlignmentPoint = new Point3d(0.5, 4.5, 0);
            txt2.TextString = value.Value;
            en.Add(txt2);

            var textArea = ProxyCAD.GetTextArea(txt2);
            var pl1 = new Polyline();
            pl1.AddVertexAt(0, new Point2d(0, 5.5), 0, 0, 0);
            pl1.AddVertexAt(0, new Point2d(0, 0), 0, 0, 0);
            pl1.AddVertexAt(0, new Point2d(-2, 3), 0, 0, 0);
            pl1.AddVertexAt(0, new Point2d(textArea[1].X - textArea[0].X + 0.5, 3), 0, 0, 0);
            en.Add(pl1);

            entityList = en;
        }

        protected override JigMark GetMarkJig()
        {
            return new JigVerticalMirrorMark(entityList, new Point3d(0, 0, 0), new AttributeToDBTextConverter());
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
    }
}

