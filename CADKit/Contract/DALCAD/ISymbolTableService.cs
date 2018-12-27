using ZwSoft.ZwCAD.DatabaseServices;

namespace CADKitCore.Contract.DALCAD
{
    public interface ISymbolTableService
    {
        ObjectId GetRecord(string symbolName);
        ObjectId GetRecord<TEnum>(TEnum _enum);
        ObjectId CreateRecord<TRecord>(TRecord symbol) where TRecord : SymbolTableRecord;
    }
}
