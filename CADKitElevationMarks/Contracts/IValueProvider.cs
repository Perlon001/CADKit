using CADKitElevationMarks.Models;

#if ZwCAD
using ZwSoft.ZwCAD.Geometry;
#endif

#if AutoCAD
using Autodesk.AutoCAD.Geometry;
#endif

namespace CADKitElevationMarks.Contracts
{
    public interface IValueProvider
    {
        ElevationValue ElevationValue { get; }
        Point3d BasePoint { get; }
        void PrepareValue();
    }
}
