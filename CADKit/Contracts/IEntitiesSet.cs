#if ZwCAD
using ZwSoft.ZwCAD.DatabaseServices;
using ZwSoft.ZwCAD.Geometry;
#endif

#if AutoCAD
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
#endif

namespace CADKit.Contracts
{
    public interface IEntitiesSet
    {
        Group ToGroup();
        BlockTableRecord ToBlock(string name);
    }
}
