using CADProxy;
using System;
using System.Collections.Generic;

#if ZwCAD
using ZwSoft.ZwCAD.DatabaseServices;
using ZwSoft.ZwCAD.EditorInput;
using ZwSoft.ZwCAD.Geometry;
using ZwSoft.ZwCAD.GraphicsInterface;
#endif

#if AutoCAD
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.GraphicsInterface;
#endif

namespace CADKit.Utils
{
    public class EntityListJig : DrawJig
    {
        protected readonly Point3d basePoint;
        protected Point3d currentPoint;
        protected IEnumerable<Entity> entityList;
        protected Matrix3d transforms;

        public EntityListJig(IEnumerable<Entity> _entityList, Point3d _basePoint)
        {
            entityList = _entityList;
            basePoint = _basePoint;
            currentPoint = _basePoint;
            transforms = Matrix3d.Displacement(basePoint.GetVectorTo(currentPoint));
        }

        public virtual IEnumerable<Entity> GetEntity()
        {
            foreach (var e in entityList)
            {
                e.TransformBy(transforms);
            }

            return entityList;
        }

        public Point3d Origin { get { return currentPoint; } }

        protected override SamplerStatus Sampler(JigPrompts prompts)
        {
            JigPromptPointOptions jigOpt = new JigPromptPointOptions("Wskaż punkt wstawienia:");
            jigOpt.UserInputControls = UserInputControls.Accept3dCoordinates;
            jigOpt.BasePoint = basePoint;

            PromptPointResult res = prompts.AcquirePoint(jigOpt);

            if (res.Value.IsEqualTo(basePoint))
            {
                return SamplerStatus.NoChange;
            }
            currentPoint = res.Value;

            return SamplerStatus.OK;
        }

        protected override bool WorldDraw(WorldDraw draw)
        {
            try
            {
                transforms = Matrix3d.Displacement(basePoint.GetVectorTo(currentPoint));
                var geometry = draw.Geometry;
                if (geometry != null)
                {
                    geometry.PushModelTransform(transforms);
                    foreach (var entity in entityList)
                    {
                        geometry.Draw(entity);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                ProxyCAD.Editor.WriteMessage(ex.Message);
                return false;
            }
        }
    }
}
