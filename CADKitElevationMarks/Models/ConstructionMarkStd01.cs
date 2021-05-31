using System;
using System.Collections.Generic;
using System.Linq;
using CADKit.Extensions;
using CADKit.Models;
using CADKit.Proxy;

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
    public class ConstructionMarkStd01 : FinishMarkStd01
    {
        public ConstructionMarkStd01(ElevationValueProvider _provider) : base(_provider) { }

        protected override void SetComponentsEntity()
        {
            base.SetComponentsEntity();
            AddHatchingArrow();
        }

        private void AddHatchingArrow()
        {
            var hatch = new Hatch();
            using (var tr = CADProxy.Database.TransactionManager.StartTransaction())
            {
                var bd = new Polyline();
                bd.AddVertexAt(0, new Point2d(0, 0), 0, 0, 0);
                bd.AddVertexAt(0, new Point2d(-2, 3), 0, 0, 0);
                bd.AddVertexAt(0, new Point2d(0, 3), 0, 0, 0);
                bd.Closed = true;
                BlockTable bt = tr.GetObject(CADProxy.Database.BlockTableId, OpenMode.ForRead) as BlockTable;
                BlockTableRecord btr = tr.GetObject(CADProxy.Database.CurrentSpaceId, OpenMode.ForWrite) as BlockTableRecord;
                var bdId = btr.AppendEntity(bd);
                tr.AddNewlyCreatedDBObject(bd, true);
                ObjectIdCollection ObjIds = new ObjectIdCollection
                {
                    bdId
                };

                hatch.SetDatabaseDefaults();
                hatch.SetHatchPattern(HatchPatternType.PreDefined, "SOLID");
                hatch.Associative = false;
                hatch.AppendLoop((int)HatchLoopTypes.Default, ObjIds);
                hatch.EvaluateHatch(true);
                bd.Erase();
            }

            var component = new MarkComponent("Wypełnienie")
            {
                Title = "Wypełnienie grota",
            };
            component.Properties.Add("Layer", "0");
            component.Properties.Add("Color", "BYLAYER");
            components.Add(component);
        }
    }
}
