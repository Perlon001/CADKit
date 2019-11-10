using CADKit.ServiceCAD.Contracts;
#if ZwCAD
using ZwSoft.ZwCAD.DatabaseServices;
#endif
#if AutoCAD
using Autodesk.AutoCAD.DatabaseServices;
#endif

namespace CADKitElevationMarks.Contracts
{
    public interface IElevationMarkTextStyleGenerator : ISymbolTableService<TextStyleTable>
    {
    }
}
