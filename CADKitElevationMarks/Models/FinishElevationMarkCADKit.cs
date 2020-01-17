using System.Linq;
using System.Collections.Generic;

using CADProxy;
using CADKit.Utils;
using CADKitElevationMarks.Contracts;
using CADKit.Models;

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
        public FinishElevationMarkCADKit() : base() { }

        public override DrawingStandards DrawingStandard { get { return DrawingStandards.CADKit; } }

        public override MarkTypes MarkType { get { return MarkTypes.finish; } }

        public override void CreateEntityList()
        {
            var en = new List<Entity>();
            var pl1 = new Polyline();

            var tx1 = new DBText();
            tx1.SetDatabaseDefaults();
            tx1.TextStyle = ProxyCAD.Database.Textstyle;
            tx1.HorizontalMode = TextHorizontalMode.TextRight;
            tx1.VerticalMode = TextVerticalMode.TextVerticalMid;
            tx1.ColorIndex = 7;
            tx1.Height = 2;
            tx1.AlignmentPoint = new Point3d(-0.5, 4.5, 0);
            tx1.TextString = this.value.Sign;
            en.Add(tx1);
            
            var tx2 = new DBText();
            tx2.SetDatabaseDefaults();
            tx2.TextStyle = ProxyCAD.Database.Textstyle;
            tx2.HorizontalMode = TextHorizontalMode.TextLeft;
            tx2.VerticalMode = TextVerticalMode.TextVerticalMid;
            tx2.ColorIndex = 7;
            tx2.Height = 2;
            tx2.AlignmentPoint = new Point3d(0.5, 4.5, 0);
            tx2.TextString = this.value.Value;
            en.Add(tx2);

            var textArea = ProxyCAD.GetTextArea(tx2);
            pl1.AddVertexAt(0, new Point2d(0, 5.5), 0, 0, 0);
            pl1.AddVertexAt(0, new Point2d(0, 0), 0, 0, 0);
            pl1.AddVertexAt(0, new Point2d(-2, 3), 0, 0, 0);
            pl1.AddVertexAt(0, new Point2d(textArea[1].X - textArea[0].X + 0.5, 3), 0, 0, 0);
            en.Add(pl1);

            this.entityList = en;
        }

        protected override EntityListJig GetMarkJig()
        {
            throw new System.NotImplementedException();
        }

        protected override void SetAttributeValue(BlockReference blockReference)
        {
            throw new System.NotImplementedException();
        }
    }
}

