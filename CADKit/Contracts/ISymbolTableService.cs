using System.Collections.Generic;

#if ZwCAD
using ZwSoft.ZwCAD.DatabaseServices;
#endif

#if AutoCAD
using Autodesk.AutoCAD.DatabaseServices;
#endif

namespace CADKit.Contracts
{
    public interface ISymbolTableService<TTable> where TTable : SymbolTable
    {
        ObjectId CreateRecord<TRecord>(TRecord symbol) where TRecord : SymbolTableRecord;

        ObjectId GetRecord<TRecord>(string symbolName) where TRecord : SymbolTableRecord;

        TRecord GetSymbol<TRecord>(string symbolName) where TRecord : SymbolTableRecord;
        IList<SymbolTableRecord> GetRecords();
    }
}
