using System.Collections.Generic;

#if ZwCAD
using ZwSoft.ZwCAD.DatabaseServices;
#endif

#if AutoCAD
using Autodesk.AutoCAD.DatabaseServices;
#endif

namespace CADKit.Models
{
    public abstract class EntitySetCreator
    {
        public abstract IEnumerable<Entity> GetEntities();
    }
}
