using CADKitElevationMarks.Contracts;
using System;
using System.Collections.Generic;
#if ZwCAD
using ZwSoft.ZwCAD.DatabaseServices;
using ZwSoft.ZwCAD.EditorInput;
using ZwSoft.ZwCAD.Geometry;
#endif
#if AutoCAD
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
#endif

namespace CADKitElevationMarks.Models
{
    public class PlaneElevationMarkCADKit : ElevationMark
    {
        protected override IEnumerable<Entity> GetEntityList()
        {
            throw new NotImplementedException();
        }

        protected override MarkJig GetMarkJig(Group group, Point3d point)
        {
            throw new NotImplementedException();
        }
    }
}
