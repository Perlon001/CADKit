using System;
using System.Collections.Generic;
using System.Linq;
using CADProxy;
using CADProxy.Internal;
using CADKit;
using CADKit.Models;
using CADKit.Services;
using CADKit.Extensions;
using CADKit.Utils;
using CADKitElevationMarks.Contracts;

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
    public abstract class ElevationMark : IElevationMark
    {
        protected PromptPointResult basePoint;
        protected ElevationValue value;
        protected IEnumerable<Entity> entityList;
        protected abstract void CreateEntityList();
        protected abstract EntityListJig GetMarkJig(Group group, Point3d point);
        public DrawingStandards DrawingStandard { get; protected set; }
        public MarkTypes MarkType { get; protected set; }

        public virtual void Create()
        {
            var variables = SystemVariableService.GetActualSystemVariables();
            try
            {
                var promptPointOptions = new PromptPointOptions("Wskaż punkt wysokościowy:");
                basePoint = ProxyCAD.Editor.GetPoint(promptPointOptions);
                if (basePoint.Status == PromptStatus.OK)
                {
                    value = new ElevationValue(GetElevationSign(), GetElevationValue());
                    using (ProxyCAD.Document.LockDocument())
                    {
                        CreateEntityList();
                        var group = entityList
                            .TransformBy(Matrix3d.Scaling(AppSettings.Instance.ScaleFactor, new Point3d(0, 0, 0)))
                            .TransformBy(Matrix3d.Displacement(new Point3d(0, 0, 0).GetVectorTo(basePoint.Value)))
                            .ToList()
                            .ToGroup();
                        using (var tr = ProxyCAD.Document.TransactionManager.StartTransaction())
                        {
                            var jig = GetMarkJig(group, basePoint.Value);
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
            } 
            catch(Exception ex)
            {
                ProxyCAD.Editor.WriteMessage(ex.Message);
            }
            finally
            {
                SystemVariableService.RestoreSystemVariables(variables);
            }
        }

        private double GetElevationValue()
        {
            return Math.Round(Math.Abs(basePoint.Value.Y) * GetElevationFactor(), 3);
        }

        private string GetElevationSign()
        {
            if (Math.Round(Math.Abs(basePoint.Value.Y) * GetElevationFactor(), 3) == 0)
            {
                return "%%p";
            }
            else if (basePoint.Value.Y < 0)
            {
                return "-";
            }
            else
            {
                return "+";
            }
        }

        private double GetElevationFactor()
        {
            switch (AppSettings.Instance.DrawingUnit)
            {
                case Units.m:
                    return 1;
                case Units.cm:
                    return 0.01;
                case Units.mm:
                    return 0.001;
                default:
                    throw new Exception("\nNie rozpoznana jednostka rysunkowa");
            }
        }

        protected void GroupErase(Transaction tr, Group group)
        {
            foreach (var id in group.GetAllEntityIds())
            {
                if (!id.IsErased)
                {
                    tr.GetObject(id, OpenMode.ForWrite).Erase();
                }
            }
            group.Erase(true);
        }
    }
}
