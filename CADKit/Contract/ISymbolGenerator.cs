using ZwSoft.ZwCAD.DatabaseServices;

namespace CADKitCore.Contract
{
    public interface ISymbolGenerator
    {
        ObjectId Generate<TRecord>() where TRecord : SymbolTableRecord;
        ObjectId Generate<TRecord>(TRecord symbol) where TRecord : SymbolTableRecord;
    }
}
