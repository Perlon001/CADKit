using System.Collections.Generic;

#if ZwCAD
using ZwSoft.ZwCAD.DatabaseServices;
#endif

#if AutoCAD
using Autodesk.AutoCAD.DatabaseServices;
#endif

namespace CADKit.Contracts.Services
{
    public interface IEntityConvert
    {
        IEnumerable<Entity> Convert(IEnumerable<Entity> entities);
    }
}
