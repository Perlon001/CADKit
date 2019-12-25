using CADKit.Models;
using CADKit.Utils;
using CADKitElevationMarks.Contracts;
using CADProxy;
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
    public class PlaneElevationMarkCADKit : ElevationMark, IElevationMark
    {
        public PlaneElevationMarkCADKit()
        {
            DrawingStandard = DrawingStandards.CADKit;
            MarkType = MarkTypes.area;
        }

        protected override void CreateEntityList()
        {
            var en = new List<Entity>();

            var tx1 = new DBText();
            tx1.SetDatabaseDefaults();
            tx1.TextStyle = ProxyCAD.Database.Textstyle;
            tx1.HorizontalMode = TextHorizontalMode.TextLeft;
            tx1.VerticalMode = TextVerticalMode.TextVerticalMid;
            tx1.ColorIndex = 7;
            tx1.Height = 2;
            tx1.AlignmentPoint = new Point3d(1, 2, 0);
            tx1.TextString = this.value.Sign + this.value.Value;
            en.Add(tx1);

            var l1 = new Line(new Point3d(-2.5, 0, 0), new Point3d(2.5, 0, 0));
            en.Add(l1);

            var l2 = new Line(new Point3d(0, -2.5, 0), new Point3d(0, 2.5, 0));
            en.Add(l2);

            var c1 = new Circle(new Point3d(0, 0, 0), new Vector3d(0, 0, 1), 1.5);
            en.Add(c1);

            AddHatching(en);

            this.entityList = en;
        }

        protected override EntityListJig GetMarkJig(Group _group, Point3d _point)
        {
            return new EntityListJig(
                _group.GetAllEntityIds()
                .Select(ent => (Entity)ent
                .GetObject(OpenMode.ForWrite)
                .Clone())
                .ToList(),
                _point);
        }

        private void AddHatching(IList<Entity> en)
        {
            var hatch = new Hatch();
            using (var tr = ProxyCAD.Database.TransactionManager.StartTransaction())
            {
                var p1 = new Polyline();
                p1.AddVertexAt(0, new Point2d(0, -1.5), 0, 0, 0);
                p1.AddVertexAt(0, new Point2d(0, 1.5), 0, 0, 0);
                p1.AddVertexAt(0, new Point2d(1.5, 0), Bulge.GetBulge(new Point2d(0, 0), new Point2d(0, 1.5), new Point2d(1.5, 0)), 0, 0);
                p1.AddVertexAt(0, new Point2d(-1.5, 0), 0, 0, 0);
                p1.AddVertexAt(0, new Point2d(0, -1.5), Bulge.GetBulge(new Point2d(0, 0), new Point2d(-1.5, 0), new Point2d(0, -1.5)), 0, 0);
                p1.Closed = true;
                BlockTable bt = tr.GetObject(ProxyCAD.Database.BlockTableId, OpenMode.ForRead) as BlockTable;
                BlockTableRecord btr = tr.GetObject(ProxyCAD.Database.CurrentSpaceId, OpenMode.ForWrite) as BlockTableRecord;
                var bdId = btr.AppendEntity(p1);
                tr.AddNewlyCreatedDBObject(p1, true);
                ObjectIdCollection ObjIds = new ObjectIdCollection();
                ObjIds.Add(bdId);

                hatch.SetDatabaseDefaults();
                hatch.SetHatchPattern(HatchPatternType.PreDefined, "SOLID");
                hatch.Associative = false;
                hatch.AppendLoop((int)HatchLoopTypes.Default, ObjIds);
                hatch.EvaluateHatch(true);
                p1.Erase();
            }
            en.Add(hatch);
        }
    }
}
