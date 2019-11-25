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
#endif

namespace CADKitElevationMarks.Models
{
    public class JigVMirrorElevationMark : DrawJig
    {
        protected override SamplerStatus Sampler(JigPrompts prompts)
        {
            throw new NotImplementedException();
        }

        protected override bool WorldDraw(WorldDraw draw)
        {
            throw new NotImplementedException();
        }
    }
}
