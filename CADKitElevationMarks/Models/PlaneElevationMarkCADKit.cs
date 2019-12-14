using CADKit.Utils;
using CADKitElevationMarks.Contracts;
using CADKitElevationMarks.Modelsm;
using System;
#if ZwCAD
using ZwSoft.ZwCAD.DatabaseServices;
using ZwSoft.ZwCAD.Geometry;
#endif
#if AutoCAD
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
#endif

namespace CADKitElevationMarks.Models
{
    public class PlaneElevationMarkCADKit : ElevationMark, IElevationMark
    {
        protected override void CreateEntityList()
        {
            throw new NotImplementedException();
        }

        protected override EntityListJig GetMarkJig(Group group, Point3d point)
        {
            throw new NotImplementedException();
        }
    }
}
