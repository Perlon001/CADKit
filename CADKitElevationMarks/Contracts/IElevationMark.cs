using System.Collections.Generic;

#if ZwCAD
using ZwSoft.ZwCAD.DatabaseServices;
#endif

#if AutoCAD
using Autodesk.AutoCAD.DatabaseServices;
#endif

namespace CADKitElevationMarks.Contracts
{
    public interface IElevationMark
    {
        string Sign { get; }
        string Value { get; }
        IEnumerable<Entity> EntityList { get; }
    }
}
