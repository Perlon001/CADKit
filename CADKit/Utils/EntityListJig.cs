using CADKit.Contracts.Services;
using CADProxy;
using CADProxy.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

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
    public abstract class EntityListJig : DrawJig
    {
        protected readonly Point3d basePoint;
        protected readonly IEnumerable<Entity> entityList;
        protected Point3d currentPoint;
        protected Matrix3d transforms;

        public Point3d JigPointResult { get { return currentPoint; } }

        protected EntityListJig(IEnumerable<Entity> _entityList, Point3d _basePoint, IEntityConvert converter = null) : base()
        {
            basePoint = _basePoint;
            entityList = _entityList.Clone();
            if (converter != null)
            {
                entityList = converter.Convert(entityList);
            }
            entityList.TransformBy(Matrix3d.Displacement(new Point3d(0, 0, 0).GetVectorTo(_basePoint)));
            ProxyCAD.UsingTransaction(PrepareEntity);
        }

        public virtual string GetSuffix()
        {
            return "";
        }

        public virtual IEnumerable<Entity> GetEntities()
        {
            var result = new List<Entity>();
            entityList.Clone().ForEach(x => 
            { 
                x.TransformBy(transforms);
                result.Add(x);
            });

            return result;
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

        #region private methods
        private void PrepareEntity(Transaction tr)
        {
            var btr = tr.GetObject(ProxyCAD.Database.CurrentSpaceId, OpenMode.ForWrite) as BlockTableRecord;
            foreach (var ent in entityList)
            {
                btr.AppendEntity(ent);
                tr.AddNewlyCreatedDBObject(ent, true);
                ent.Erase(true);
            }
        }
        #endregion
    }
}
