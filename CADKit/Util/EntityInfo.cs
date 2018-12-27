using ZwSoft.ZwCAD.DatabaseServices;
using ZwSoft.ZwCAD.Geometry;

namespace CADKit.Util
{
    public static class EntityInfo
    {
        public static Point3d[] GetTextArea(DBText text)
        {
            Point3d leftBottom = text.GeometricExtents.MinPoint;
            Point3d rightTop = text.GeometricExtents.MaxPoint;

            return new Point3d[2] { leftBottom, rightTop };
        }
    }
}
