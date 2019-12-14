using CADKit.Utils;
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
    public class JigDisplacement : EntityListJig
    {
        public JigDisplacement(IEnumerable<Entity> _entityList, Point3d _basePoint) : base(_entityList, _basePoint) { }
        
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
            transforms = Matrix3d.Displacement(basePoint.GetVectorTo(currentPoint));
            return base.WorldDraw(draw);
        }

    }
}
