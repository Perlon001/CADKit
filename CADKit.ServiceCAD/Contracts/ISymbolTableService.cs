using System.Collections.Generic;

#if ZwCAD
using DatabaseServices = ZwSoft.ZwCAD.DatabaseServices;
#endif

#if AutoCAD
using Autodesk.AutoCAD.DatabaseServices;
#endif

namespace CADProxy.Contracts
{
    public interface ISymbolTableService<TTable> where TTable : DatabaseServices.SymbolTable
    {
        DatabaseServices.ObjectId CreateRecord<TRecord>(TRecord symbol) where TRecord : DatabaseServices.SymbolTableRecord;

        DatabaseServices.ObjectId GetRecord<TRecord>(string symbolName) where TRecord : DatabaseServices.SymbolTableRecord;

        TRecord GetSymbol<TRecord>(string symbolName) where TRecord : DatabaseServices.SymbolTableRecord;
        IList<DatabaseServices.SymbolTableRecord> GetRecords();
    }
}
