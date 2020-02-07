using System.Collections.Generic;

#if ZwCAD
using ZwSoft.ZwCAD.DatabaseServices;
#endif

#if AutoCAD
using Autodesk.AutoCAD.DatabaseServices;
#endif

namespace CADKit.Contracts
{
    public interface IEntityConverter
    {
        IEnumerable<Entity> Convert(IEnumerable<Entity> entities);
    }
}
