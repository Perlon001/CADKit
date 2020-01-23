using CADKit.Contracts;
using System;
using System.Collections.Generic;

#if ZwCAD
using ZwSoft.ZwCAD.ApplicationServices;
using ZwSoft.ZwCAD.DatabaseServices;
#endif

#if AutoCAD
using Autodesk.AutoCAD.ApplicationServices;
using utodesk.AutoCAD.DatabaseServices;
#endif

namespace CADKit.Services
{
    public abstract class SymbolTableService<TTable> : ISymbolTableService<TTable> where TTable : SymbolTable
    {
        private Dictionary<string, SymbolTableRecord> recordDict;

        public SymbolTableService()
        {
            recordDict = new Dictionary<string, SymbolTableRecord>();
        }

        public ObjectId CreateRecord<TRecord>(TRecord symbol) where TRecord : SymbolTableRecord
        {
            Document acDoc = Application.DocumentManager.MdiActiveDocument;
            Database acDatabase = acDoc.Database;
            ObjectId result;
            using (Transaction transaction = acDoc.TransactionManager.StartTransaction())
            {
                TTable symbolTable = (TTable)transaction.GetObject(GetObjectId(acDatabase, typeof(TTable)), mode: OpenMode.ForRead);

                if (symbolTable.Has(symbol.Name))
                {
                    result = symbolTable[symbol.Name];
                }
                else
                {
                    symbolTable.UpgradeOpen();
                    result = symbolTable.Add(symbol);
                    transaction.AddNewlyCreatedDBObject(symbol, true);
                    transaction.Commit();
                }
            }
            return result;
        }

        public ObjectId GetRecord<TRecord>(string symbolName) where TRecord : SymbolTableRecord
        {
            Document acDoc = Application.DocumentManager.MdiActiveDocument;
            Database acDatabase = acDoc.Database;
            ObjectId result;
            using (Transaction transaction = acDoc.TransactionManager.StartTransaction())
            {
                TTable symbolTable = (TTable)transaction.GetObject(GetObjectId(acDatabase, typeof(TTable)), mode: OpenMode.ForRead);

                result = symbolTable.Has(symbolName) ? symbolTable[symbolName] : ObjectId.Null;
            }
            return result;
        }

        public TRecord GetSymbol<TRecord>(string symbolName) where TRecord : SymbolTableRecord
        {
            return recordDict.ContainsKey(symbolName) ? (TRecord)recordDict[symbolName] : null;
        }

        private ObjectId GetObjectId(Database db, Type type)
        {
            switch (type.Name)
            {
                case "LayerTable":
                    return db.LayerTableId;
                case "TextStyleTable":
                    return db.TextStyleTableId;
                default:
                    throw new NotSupportedException();
            }
        }

        public virtual IList<SymbolTableRecord> GetRecords()
        {
            IList<SymbolTableRecord> result = new List<SymbolTableRecord>();
            foreach (var item in recordDict)
            {
                result.Add(item.Value);
            }

            return result;
        }
    }
}
