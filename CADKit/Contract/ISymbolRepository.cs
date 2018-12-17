using ZwSoft.ZwCAD.DatabaseServices;

namespace CADKitCore.Contract
{
    public interface ISymbolRepository : IRepository
    {
        SymbolTableRecord GetSymbol<TEnum>(TEnum _enum);
        SymbolTableRecord GetSymbol(string symbolName);
    }
}
