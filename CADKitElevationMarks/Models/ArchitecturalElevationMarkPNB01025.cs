using CADKit;
using CADKit.Utils;
using CADKitElevationMarks.Contracts;
using CADKitElevationMarks.Modelsm;
using CADProxy;
using System;
using System.Collections.Generic;
using System.Linq;

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
    public class ArchitecturalElevationMarkPNB01025 : ElevationMark, IElevationMark
    {
        protected override void CreateEntityList()
        {
            var en = new List<Entity>();
            var tx1 = new DBText();
            var tx2 = new DBText();
            var pl1 = new Polyline();
            var pl2 = new Polyline();

            tx1.SetDatabaseDefaults();
            tx1.TextStyle = ProxyCAD.Database.Textstyle;
            tx1.HorizontalMode = TextHorizontalMode.TextLeft;
            tx1.VerticalMode = TextVerticalMode.TextVerticalMid;
            tx1.ColorIndex = 7;
            tx1.Height = 2;
            tx1.AlignmentPoint = new Point3d(2, 4.5, 0);
            tx1.TextString = this.value.Value;
            en.Add(tx1);

            tx2.SetDatabaseDefaults();
            tx2.TextStyle = ProxyCAD.Database.Textstyle;
            tx2.HorizontalMode = TextHorizontalMode.TextRight;
            tx2.VerticalMode = TextVerticalMode.TextVerticalMid;
            tx2.ColorIndex = 7;
            tx2.Height = 2;
            tx2.AlignmentPoint = new Point3d(1.5, 4.5, 0);
            tx2.TextString = this.value.Sign;
            en.Add(tx2);

            pl1.AddVertexAt(0, new Point2d(-1.5, 1.5), 0, 0, 0);
            pl1.AddVertexAt(0, new Point2d(0, 0), 0, 0, 0);
            pl1.AddVertexAt(0, new Point2d(1.5, 1.5), 0, 0, 0);
            if (Math.Round(Math.Abs(this.basePoint.Value.Y) * AppSettings.Instance.ScaleFactor, 3) == 0)
            {
                pl1.Closed = true;
                AddHatchingArrow(en);
            }
            en.Add(pl1);

            var textArea = EntityInfo.GetTextArea(tx1);
            pl2 = new Polyline();
            pl2.AddVertexAt(0, new Point2d(0, 0), 0, 0, 0);
            pl2.AddVertexAt(0, new Point2d(0, 3), 0, 0, 0);
            pl2.AddVertexAt(0, new Point2d(textArea[1].X - textArea[0].X + 2, 3), 0, 0, 0);
            en.Add(pl2);
            
            this.entityList = en;
        }

        protected override EntityListJig GetMarkJig(Group _group, Point3d _point)
        {
            return new JigVerticalConstantVerticalAndHorizontalMirrorMark(
                _group.GetAllEntityIds()
                .Select(ent => (Entity)ent
                .GetObject(OpenMode.ForWrite)
                .Clone())
                .ToList(),
                _point);
        }

        private void AddHatchingArrow(IList<Entity> en)
        {
            var hatch = new Hatch();
            using (var tr = ProxyCAD.Database.TransactionManager.StartTransaction())
            {
                var bd = new Polyline();
                bd.AddVertexAt(0, new Point2d(0, 0), 0, 0, 0);
                bd.AddVertexAt(0, new Point2d(1.5, 1.5), 0, 0, 0);
                bd.AddVertexAt(0, new Point2d(0, 1.5), 0, 0, 0);
                bd.Closed = true;
                BlockTable bt = tr.GetObject(ProxyCAD.Database.BlockTableId, OpenMode.ForRead) as BlockTable;
                BlockTableRecord btr = tr.GetObject(ProxyCAD.Database.CurrentSpaceId, OpenMode.ForWrite) as BlockTableRecord;
                var bdId = btr.AppendEntity(bd);
                tr.AddNewlyCreatedDBObject(bd, true);
                ObjectIdCollection ObjIds = new ObjectIdCollection();
                ObjIds.Add(bdId);

                hatch.SetDatabaseDefaults();
                hatch.SetHatchPattern(HatchPatternType.PreDefined, "SOLID");
                hatch.Associative = false;
                hatch.AppendLoop((int)HatchLoopTypes.Default, ObjIds);
                hatch.EvaluateHatch(true);
                bd.Erase();
            }
            en.Add(hatch);
        }
    }
}

    