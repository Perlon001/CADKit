using CADKit.Models;
using CADKit.Services;
using CADKit.Utils;
using CADKitElevationMarks.Contracts;
using CADProxy;
using System.Collections.Generic;
using System.Linq;
using CADKit.Extensions;
using CADKit;
using CADProxy.Internal;
using System;

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
    public class PlaneElevationMarkCADKit : ElevationMark, IElevationMark
    {
        public PlaneElevationMarkCADKit()
        {
            DrawingStandard = DrawingStandards.CADKit;
            MarkType = MarkTypes.area;
        }

        public override void Create()
        {
            var variables = SystemVariableService.GetActualSystemVariables();
            try
            {
                value = new ElevationValue("%%p", 0.00);
                using (ProxyCAD.Document.LockDocument())
                {
                    CreateEntityList();
                    var group = entityList
                        .TransformBy(Matrix3d.Scaling(AppSettings.Instance.ScaleFactor, new Point3d(0, 0, 0)))
                        .ToList()
                        .ToGroup();
                    using (var tr = ProxyCAD.Document.TransactionManager.StartTransaction())
                    {
                        var jig = GetMarkJig(group, new Point3d(0, 0, 0));
                        (group.ObjectId.GetObject(OpenMode.ForWrite) as Group).SetVisibility(false);
                        var result = ProxyCAD.Editor.Drag(jig);
                        GroupErase(tr, group);
                        if (result.Status == PromptStatus.OK)
                        {
                            group = jig.GetEntity().ToList().ToGroup();
                        }
                        Utils.FlushGraphics();
                        tr.Commit();
                    }
                }
            }
            catch (Exception ex)
            {
                ProxyCAD.Editor.WriteMessage(ex.Message);
            }
            finally
            {
                SystemVariableService.RestoreSystemVariables(variables);
            }
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
