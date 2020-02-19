using System.Collections.Generic;
using CADKit.Extensions;
using CADKit.Contracts;
using CADKit.Proxy;
using System;
using System.Linq;

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
        protected IEnumerable<Entity> buffer;
        protected IEnumerable<Entity> entities;
        protected Point3d originPoint;
        protected Point3d basePoint;
        protected Point3d currentPoint;
        protected Matrix3d transform;
        protected IList<Matrix3d> transforms;
        protected IEnumerable<IEntityConverter> converters;

        public IEnumerable<Entity> GetEntity()
        {
            return buffer;
        }

        public Point3d JigPointResult { get { return currentPoint; } }

        public IList<Matrix3d> Transforms { get { return transforms; } }

        public IEnumerable<IEntityConverter> Converters { get { return converters; } set { converters = value; } }

        public EntittiesJig(IEnumerable<Entity> _entities, Point3d _originPoint, Point3d _basePoint = default, IEnumerable<IEntityConverter> _converters = null) : base()
        {
            buffer = _entities;
            entities = _entities.Clone().ToList();
            originPoint = _originPoint;
            basePoint = _basePoint;
            converters = _converters;
            if (converters != null)
            {
                converters.ForEach(x => entities = x.Convert(entities));
            }
            transforms = new List<Matrix3d>();
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
            switch (res.Status)
            {
                case PromptStatus.OK:
                    currentPoint = res.Value;
                    return SamplerStatus.OK;
                case PromptStatus.Cancel:
                    throw new OperationCanceledException();
                default:
                    throw new Exception("Nie rozpoznany PromptPoint ResultStatus");
            }
        }

        protected override bool WorldDraw(WorldDraw _draw)
        {
            transform = Matrix3d.Displacement(new Vector3d(currentPoint.X, currentPoint.Y, currentPoint.Z));
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

        #region private methods
        protected void PrepareEntity(Transaction tr)
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
