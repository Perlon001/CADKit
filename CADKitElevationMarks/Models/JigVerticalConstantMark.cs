using System.Collections.Generic;

#if ZwCAD
using ZwSoft.ZwCAD.DatabaseServices;
using ZwSoft.ZwCAD.Geometry;
using ZwSoft.ZwCAD.GraphicsInterface;
#endif

#if AutoCAD
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.GraphicsInterface;
#endif

namespace CADKitElevationMarks.Models
{
    public class JigVerticalConstantMark : JigMark
    {
        public JigVerticalConstantMark(IEnumerable<Entity> _group, Point3d _basePoint) : base(_group, _basePoint) { }

        protected override bool WorldDraw(WorldDraw draw)
        {
            transform = Matrix3d.Displacement(basePoint.GetVectorTo(new Point3d(currentPoint.X, basePoint.Y, currentPoint.Z)));
            return base.WorldDraw(draw);
        }
    }
}
