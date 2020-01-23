using System.Collections.Generic;


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
using Autodesk.AutoCAD.GraphicsInterface;
#endif
namespace CADKit.Utils
{
    public class JigDisplacement : MarkJig
    {
        public JigDisplacement(IEnumerable<Entity> _entityList, Point3d _basePoint) : base(_entityList, _basePoint) { }
        
        protected override bool WorldDraw(WorldDraw draw)
        {
            transforms = Matrix3d.Displacement(basePoint.GetVectorTo(currentPoint));
            return base.WorldDraw(draw);
        }

    }
}
