#if ZwCAD
using CADDatabaseServices = ZwSoft.ZwCAD.Geometry;
#endif
#if AutoCAD
using CADDatabaseServices = Autodesk.AutoCAD.Geometry;
#endif

namespace CADKit.Geometry
{
    public struct Point3d
    {
        public Point3d(double[] xyz) { }
        public Point3d(CADDatabaseServices.PlanarEntity plane, CADDatabaseServices.Point2d point) { }
        public Point3d(double x, double y, double z) { }

    }
}
