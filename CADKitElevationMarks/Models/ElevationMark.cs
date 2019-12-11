using System;
using System.Collections.Generic;
using System.Linq;
using CADProxy;
using CADKit;
using CADKit.Models;
using CADKit.Services;
using CADKit.Extensions;
using CADKitElevationMarks.Contracts;
using CADKitElevationMarks.Models;

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

namespace CADKitElevationMarks.Modelsm
{
    public abstract class ElevationMark
    {
        protected readonly SystemVariables variables;
        protected readonly PromptPointResult basePoint;
        protected ElevationValue value;
        protected IElevationMarkConfig config;
        protected IEnumerable<Entity> entityList;

        //public string Sign { get { return value.Sign; } }

        //public string Value { get { return value.Value; } }

        //public IEnumerable<Entity> EntityList 
        //{ 
        //    get 
        //    {
        //        return entityList;
        //    } 
        //}
        public ElevationMark()
        {
            variables = SystemVariableService.GetSystemVariables();
            basePoint = GetBasePoint();
        }

        public void Create()
        {
            if (basePoint.Status == PromptStatus.OK)
            {
                value = new ElevationValue(GetElevationSign(), GetElevationValue());
                CreateEntityList();
                var group = entityList
                    .TransformBy(Matrix3d.Scaling(AppSettings.Instance.ScaleFactor,new Point3d(0,0,0)))
                    .TransformBy(Matrix3d.Displacement(new Point3d(0, 0, 0).GetVectorTo(basePoint.Value)))
                    .ToList()
                    .ToGroup();
                using (var tr = ProxyCAD.Document.TransactionManager.StartTransaction())
                {
                    var jig = GetMarkJig(group, basePoint.Value);
                    (group.ObjectId.GetObject(OpenMode.ForWrite) as Group).SetVisibility(false);
                    var result = ProxyCAD.Editor.Drag(jig);
                    if (result.Status == PromptStatus.OK)
                    {
                        foreach (var p in entityList)
                        {
                            p.TransformBy(jig.Transforms);
                        }
                        (group.ObjectId.GetObject(OpenMode.ForWrite) as Group).SetVisibility(true);
                    }
                    else
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
                    tr.Commit();
                }

            }
            SystemVariableService.RestoreSystemVariables(variables);
        }

        protected abstract void CreateEntityList();
        protected abstract MarkJig GetMarkJig(Group group, Point3d point);

        private PromptPointResult GetBasePoint()
        {
            var promptPointOptions = new PromptPointOptions("Wskaż punkt wysokościowy:");
            var basePoint = ProxyCAD.Editor.GetPoint(promptPointOptions);

            return basePoint;
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

        protected double GetElevationFactor()
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

    }
}
