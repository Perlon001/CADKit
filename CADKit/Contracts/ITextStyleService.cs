#if ZwCAD
using ZwSoft.ZwCAD.DatabaseServices;
#endif

#if AutoCAD
using Autodesk.AutoCAD.DatabaseServices;
#endif

namespace CADKit.Contracts
{
    public interface ITextStyleService : ISymbolTableService<TextStyleTable>
    {
    }
}
