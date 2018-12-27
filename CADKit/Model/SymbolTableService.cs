using CADKitCore.Contract.DALCAD;
using System;
using ZwSoft.ZwCAD.ApplicationServices;
using ZwSoft.ZwCAD.DatabaseServices;

namespace CADKitCore.Model
{
    public abstract class SymbolTableService<TTable> : ISymbolTableService where TTable : SymbolTable
    {
        ISymbolRepository creator;

        public SymbolTableService(ISymbolRepository _creator)
        {
            this.creator = _creator;
        }

        public ObjectId GetRecord<TEnum>(TEnum _enum)
        {
            SymbolTableRecord symbol = creator.GetSymbol(_enum);
            ObjectId result = GetRecord(symbol.Name);
            if (result == ObjectId.Null)
            {
                result = CreateRecord(creator.GetSymbol(_enum));
            }

            return result;
        }

        public ObjectId GetRecord(string symbolName)
        {
            Document acDoc = Application.DocumentManager.MdiActiveDocument;
            Database acDatabase = acDoc.Database;
            ObjectId result;
            using (Transaction transaction = acDoc.TransactionManager.StartTransaction())
            {
                TTable symbolTable = (TTable)transaction.GetObject(GetObjectId(acDatabase, typeof(TTable)), mode: OpenMode.ForRead);

                if (symbolTable.Has(symbolName))
                {
                    result = symbolTable[symbolName];
                }
                else
                {
                    result = ObjectId.Null;
                }
            }
            return result;
        }

        public virtual ObjectId CreateRecord<TRecord>(TRecord symbol) where TRecord : SymbolTableRecord 
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

        private ObjectId GetObjectId(Database db, Type type)
        {
            switch(type.Name)
            {
                case "LayerTable":
                    return db.LayerTableId;
                case "TextStyleTable":
                    return db.TextStyleTableId;
                default:
                    throw new NotSupportedException();
            }
        }
    }
}
