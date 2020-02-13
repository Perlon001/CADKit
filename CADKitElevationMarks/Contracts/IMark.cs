using System.Collections.Generic;

#if ZwCAD
using ZwSoft.ZwCAD.DatabaseServices;
using ZwSoft.ZwCAD.Geometry;
#endif

#if AutoCAD
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
#endif

namespace CADKitElevationMarks.Contracts
{
    public interface IMark
    {
        string Index { get; }
        Point3d BasePoint { get; }
        IEnumerable<Entity> GetEntities();
    }
}
