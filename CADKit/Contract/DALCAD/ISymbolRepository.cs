using ZwSoft.ZwCAD.DatabaseServices;

namespace CADKitCore.Contract.DALCAD
{
    public interface ISymbolRepository : IRepository
    {
        SymbolTableRecord GetSymbol<TEnum>(TEnum _enum);
        SymbolTableRecord GetSymbol(string symbolName);
    }
}
