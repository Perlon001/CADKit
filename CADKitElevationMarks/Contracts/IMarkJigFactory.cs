using System.Collections.Generic;

#if ZwCAD
using ZwSoft.ZwCAD.DatabaseServices;
using ZwSoft.ZwCAD.EditorInput;
using ZwSoft.ZwCAD.Geometry;
#endif

#if AutoCAD
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
#endif
namespace CADKitElevationMarks.Contracts
{
    public interface IMarkJigFactory
    {
        EntityJig GetMarkJig(ElevationMarkType type, IEnumerable<Entity> entityList, Point3d basePoint); 
    }
}
