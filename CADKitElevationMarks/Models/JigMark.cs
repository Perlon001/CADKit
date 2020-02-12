using CADKit;
using CADKit.Extensions;
using System;
using System.Collections.Generic;
using CADKit.Proxy;
using CADKit.Contracts;
using CADKit.Models;

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

namespace CADKitElevationMarks.Models
{
    public class JigMark : EntittiesJig
    {
        public JigMark(IEnumerable<Entity> _entities, Point3d _basePoint, IEnumerable<IEntityConverter> _converters = null) : base(_entities, _basePoint, _converters)
        {
            var transform = Matrix3d.Scaling(AppSettings.Get.ScaleFactor, new Point3d(0, 0, 0));
            transforms.Add(transform);
            entities.TransformBy(transform);
            transform = Matrix3d.Displacement(new Point3d(0, 0, 0).GetVectorTo(basePoint));
            entities.TransformBy(transform);
            CADProxy.UsingTransaction(PrepareEntity);
        }

        public virtual string GetSuffix()
        {
            return "";
        }

        protected override SamplerStatus Sampler(JigPrompts prompts)
        {
            JigPromptPointOptions jigOpt = new JigPromptPointOptions("Wskaż punkt wstawienia:");
            jigOpt.UserInputControls = UserInputControls.Accept3dCoordinates;
            jigOpt.BasePoint = basePoint;
            PromptPointResult res = prompts.AcquirePoint(jigOpt);
            currentPoint = res.Value;

            return SamplerStatus.OK;
        }

        protected override bool WorldDraw(WorldDraw draw)
        {
            try
            {
                transform = Matrix3d.Displacement(basePoint.GetVectorTo(currentPoint));
                var geometry = draw.Geometry;
                if (geometry != null)
                {
                    geometry.PushModelTransform(transform);
                    foreach (var entity in entities)
                    {
                        geometry.Draw(entity);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                CADProxy.Editor.WriteMessage(ex.Message);
                return false;
            }
        }
    }
}
