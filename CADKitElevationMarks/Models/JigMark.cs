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

namespace CADKitElevationMarks.Models
{
    public class JigMark : DrawJig
    {
        protected readonly Point3d basePoint;
        protected IEnumerable<Entity> entityList;
        protected IEnumerable<Entity> entityBuffer;
        protected Point3d currentPoint;
        protected Matrix3d transforms;

        public Point3d JigPointResult { get { return currentPoint; } }

        public JigMark(IEnumerable<Entity> _entityList, Point3d _basePoint, IEntityConvert _converter = null) : base()
        {
            entityBuffer = _entityList;
            entityList = _entityList.Clone();
            basePoint = _basePoint;
            if (_converter != null)
            {
                entityList = _converter.Convert(entityList);
            }
            entityList.TransformBy(Matrix3d.Displacement(new Point3d(0, 0, 0).GetVectorTo(_basePoint)));
            ProxyCAD.UsingTransaction(PrepareEntity);
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

        public IEnumerable<Entity> GetEntity()
        {
            return entityBuffer;
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
                ent.Erase();
            }
        }
        #endregion
    }
}
