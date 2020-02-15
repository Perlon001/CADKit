using CADKit;
using CADKit.Extensions;
using System;
using System.Collections.Generic;
using CADKit.Proxy;
using CADKit.Contracts;
using CADKit.Models;
using CADKitElevationMarks.Events;

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
        public virtual string Suffix => "";

        public JigMark(IEnumerable<Entity> _entities, Point3d _originPoint, Point3d _basePoint, IEnumerable<IEntityConverter> _converters = null) : base(_entities, _originPoint, _basePoint, _converters)
        {
            var transform = Matrix3d.Scaling(AppSettings.Get.ScaleFactor, new Point3d(0, 0, 0));
            transforms.Add(transform);
            entities.TransformBy(transform);
            transform = Matrix3d.Displacement(new Point3d(0, 0, 0).GetVectorTo(basePoint));
            entities.TransformBy(transform);
            CADProxy.UsingTransaction(PrepareEntity);
        }

        public event EventHandler<ChangeMarkSuffixEventArgs> ChangeMarkSuffix;

        protected virtual void OnSuffixChanged(ChangeMarkSuffixEventArgs _args)
        {
            ChangeMarkSuffix?.Invoke(this, _args);
        }

        protected override SamplerStatus Sampler(JigPrompts _prompts)
        {
            JigPromptPointOptions jigOpt = new JigPromptPointOptions("Wskaż punkt wstawienia:")
            {
                UserInputControls = UserInputControls.Accept3dCoordinates,
                BasePoint = basePoint
            };
            PromptPointResult res = _prompts.AcquirePoint(jigOpt);
            currentPoint = res.Value;

            return SamplerStatus.OK;
        }

        protected override bool WorldDraw(WorldDraw _draw)
        {
            try
            {
                transform = Matrix3d.Displacement(basePoint.GetVectorTo(currentPoint));
                var geometry = _draw.Geometry;
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
            catch (Exception _ex)
            {
                CADProxy.Editor.WriteMessage(_ex.Message);
                return false;
            }
        }
    }
}
