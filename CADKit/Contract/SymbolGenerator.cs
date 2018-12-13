using System;
using ZwSoft.ZwCAD.ApplicationServices;
using ZwSoft.ZwCAD.DatabaseServices;

namespace CADKitCore.Contract
{
    public abstract class SymbolGenerator<TTable> : ISymbolGenerator where TTable : SymbolTable
    {
        public abstract ObjectId Generate<TRecord>() where TRecord : SymbolTableRecord;

        public virtual ObjectId Generate<TRecord>(TRecord symbol) where TRecord : SymbolTableRecord
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
