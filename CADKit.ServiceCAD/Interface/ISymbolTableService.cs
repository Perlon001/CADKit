using System.Collections.Generic;
using ZwSoft.ZwCAD.DatabaseServices;

namespace CADKit.ServiceCAD.Interface
{
    public interface ISymbolTableService<TTable> where TTable : SymbolTable
    {
        ObjectId CreateRecord<TRecord>(TRecord symbol) where TRecord : SymbolTableRecord;

        ObjectId GetRecord<TRecord>(string symbolName) where TRecord : SymbolTableRecord;

        TRecord GetSymbol<TRecord>(string symbolName) where TRecord : SymbolTableRecord;
        IList<SymbolTableRecord> GetRecords();
    }
}
