using CADKit.Utils;
using CADKitElevationMarks.Contracts;
using CADProxy;
using System.Collections.Generic;
using CADKitElevationMarks.Modelsm;
using System.Linq;

#if ZwCAD
using ZwSoft.ZwCAD.DatabaseServices;
using ZwSoft.ZwCAD.Geometry;
using ZwSoft.ZwCAD.EditorInput;
#endif

#if AutoCAD
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.EditorInput;
#endif

namespace CADKitElevationMarks.Models
{
    public class ArchitecturalElevationMarkCADKit : ElevationMark, IElevationMark
    {
        protected override void CreateEntityList()
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
            tx1.AlignmentPoint = new Point3d(0.5, 4, 0);
            tx1.TextString = this.value.Sign;
            en.Add(tx1);
            
            var tx2 = new DBText();
            tx1.SetDatabaseDefaults();
            tx1.TextStyle = ProxyCAD.Database.Textstyle;
            tx1.HorizontalMode = TextHorizontalMode.TextLeft;
            tx1.VerticalMode = TextVerticalMode.TextVerticalMid;
            tx1.ColorIndex = 7;
            tx1.Height = 2;
            tx1.AlignmentPoint = new Point3d(0.5, 4, 0);
            tx1.TextString = this.value.Value;
            en.Add(tx2);

            var textArea = EntityInfo.GetTextArea(tx1);
            pl1.AddVertexAt(0, new Point2d(0, 5), 0, 0, 0);
            pl1.AddVertexAt(0, new Point2d(0, 0), 0, 0, 0);
            pl1.AddVertexAt(0, new Point2d(-2, 2.5), 0, 0, 0);
            pl1.AddVertexAt(0, new Point2d(textArea[1].X - textArea[0].X, 2.5), 0, 0, 0);
            en.Add(pl1);

            this.entityList = en;
        }

        protected override EntityListJig GetMarkJig(Group _group, Point3d _point)
        {
            return new JigVerticalConstantHorizontalMirrorMark(
                _group.GetAllEntityIds()
                .Select(ent => (Entity)ent
                .GetObject(OpenMode.ForWrite)
                .Clone())
                .ToList(),
                _point);
        }
    }
}

