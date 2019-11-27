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
namespace CADKitElevationMarks.Models
{
    public class JigDisplacement : DrawJig
    {
        protected IEnumerable<Entity> group;
        protected Point3d currentPoint;
        protected Point3d basePoint;
        protected readonly Editor ed = ProxyCAD.Editor;

        public Matrix3d Transforms { get; protected set; }

        public JigDisplacement(IEnumerable<Entity> _group, Point3d _basePoint)
        {
            group = _group;
            basePoint = _basePoint;
        }
        protected override SamplerStatus Sampler(JigPrompts prompts)
        {
            JigPromptPointOptions jigOpt = new JigPromptPointOptions("Wskaż punkt wstawienia:");
            jigOpt.UserInputControls = UserInputControls.Accept3dCoordinates;
            jigOpt.BasePoint = basePoint;

            PromptPointResult res = prompts.AcquirePoint(jigOpt);

            if (res.Value.IsEqualTo(currentPoint))
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
                    foreach (var entity in group)
                    {
                        geometry.Draw(entity);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                ed.WriteMessage(ex.Message);
                return false;
            }
        }

    }
}
