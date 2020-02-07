using System.Collections.Generic;
using CADKit.Extensions;
using CADKit.Contracts;
using CADKit.Proxy;

#if ZwCAD
using ZwSoft.ZwCAD.EditorInput;
using ZwSoft.ZwCAD.DatabaseServices;
using ZwSoft.ZwCAD.GraphicsInterface;
using ZwSoft.ZwCAD.Geometry;
#endif

#if AutoCAD
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.GraphicsInterface;
using Autodesk.AutoCAD.Geometry;
#endif

namespace CADKit.Models
{
    public class EntittiesJig : DrawJig
    {
        protected readonly IEnumerable<Entity> buffer;
        protected IEnumerable<Entity> entities;
        protected readonly Point3d basePoint;
        protected Point3d currentPoint;
        protected Matrix3d transforms;
        protected readonly IEnumerable<IEntityConverter> converters;

        public IEnumerable<Entity> GetEntity()
        {
            return buffer;
        }
        public Point3d JigPointResult { get { return currentPoint; } }
        public IEnumerable<IEntityConverter> Converters { get { return converters; } }


        public EntittiesJig(IEnumerable<Entity> _entities, Point3d _basePoint = default, IEnumerable<IEntityConverter> _converters = null) : base()
        {
            buffer = _entities;
            entities = _entities.Clone();
            converters = _converters;
            if (converters != null)
            {
                converters.ForEach(x => entities = x.Convert(entities));
            }
            CADProxy.UsingTransaction(PrepareEntity);
        }

        protected override SamplerStatus Sampler(JigPrompts _prompts)
        {
            JigPromptPointOptions jigOpt = new JigPromptPointOptions("Wskaż punkt wstawienia:")
            {
                UserInputControls = UserInputControls.Accept3dCoordinates,
            };
            jigOpt.BasePoint = basePoint;
            jigOpt.UseBasePoint = true;

            PromptPointResult res = _prompts.AcquirePoint(jigOpt);
            if (res.Status == PromptStatus.Error || res.Status == PromptStatus.Cancel)
            {
                return SamplerStatus.Cancel;
            }
            currentPoint = res.Value;

            return SamplerStatus.OK;
        }

        protected override bool WorldDraw(WorldDraw _draw)
        {
            transforms = Matrix3d.Displacement(new Vector3d(currentPoint.X, currentPoint.Y, currentPoint.Z));
            var geometry = _draw.Geometry;
            if (geometry != null)
            {
                geometry.PushModelTransform(transforms);
                foreach (var entity in entities)
                {
                    geometry.Draw(entity);
                }
            }

            return true;
        }

        #region private methods
        private void PrepareEntity(Transaction tr)
        {
            var btr = tr.GetObject(CADProxy.Database.CurrentSpaceId, OpenMode.ForWrite) as BlockTableRecord;
            foreach (var ent in entities)
            {
                btr.AppendEntity(ent);
                tr.AddNewlyCreatedDBObject(ent, true);
                ent.Erase();
            }
        }
        #endregion

    }
}
