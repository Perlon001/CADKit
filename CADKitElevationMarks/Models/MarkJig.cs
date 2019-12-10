using CADKitElevationMarks.Contracts.Views;
using CADProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    public abstract class MarkJig : DrawJig
    {
        protected Point3d currentPoint;
        protected Point3d basePoint;
        protected IEnumerable<Entity> entityList;
        public Matrix3d Transforms { get; protected set; }

        public MarkJig(IEnumerable<Entity> _entityList, Point3d _basePoint)
        {
            entityList = _entityList;
            basePoint = _basePoint;
        }

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
                Transforms = Matrix3d.Displacement(basePoint.GetVectorTo(currentPoint));
                var geometry = draw.Geometry;
                if (geometry != null)
                {
                    geometry.PushModelTransform(Transforms);
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

        protected virtual void EntityListUpdate(IEnumerable<Entity> _entityList)
        {
            entityList = _entityList;
        }
    }
}
