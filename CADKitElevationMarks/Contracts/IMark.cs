using CADKitElevationMarks.Models;
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
        ElevationValue Value { get; } 
        string Index { get; }
        Point3d BasePoint { get; }
        Point3d OriginPoint { get; }
        IEnumerable<Entity> GetEntities();
        void SetAttributeValue(BlockReference blockReference);
    }
}
