using CADKit.Utils;
using CADKitElevationMarks.Contracts;
using CADProxy;
using System.Collections.Generic;
using CADKitElevationMarks.Modelsm;

#if ZwCAD
using ZwSoft.ZwCAD.DatabaseServices;
using ZwSoft.ZwCAD.Geometry;
using ZwSoft.ZwCAD.EditorInput;
#endif

#if AutoCAD
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.EditorInput;
#endif

namespace CADKitElevationMarks.Models
{
    public class ArchitecturalElevationMarkCADKit : ElevationMark, IElevationMark
    {
        protected override void CreateEntityList()
        {
            throw new System.NotImplementedException();
        }

        protected override EntityListJig GetMarkJig(Group group, Point3d point)
        {
            throw new System.NotImplementedException();
        }
    }
}

