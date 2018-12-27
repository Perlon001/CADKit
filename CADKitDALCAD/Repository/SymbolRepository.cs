using System;
using System.Collections.Generic;
using ZwSoft.ZwCAD.DatabaseServices;

namespace CADKitDALCAD.Repository
{
    public abstract class SymbolRepository<TEnum,TRecord> : ISymbolRepository where TRecord : SymbolTableRecord
    {
        private Dictionary<string, TRecord> recordDict;

        public SymbolRepository()
        {
            recordDict = new Dictionary<string, TRecord>();
            FillDictionary();
        }

        public SymbolTableRecord GetSymbol(string symbolName)
        {
            if(recordDict.ContainsKey(symbolName))
            {
                return recordDict[symbolName];
            }

            return null;
        }

        public SymbolTableRecord GetSymbol<T>(T _enum)
        {
            if (!typeof(T).IsEnum)
                throw new ArgumentException("Argument nie jest typem wyliczeniowym");

            return recordDict[_enum.ToString()];
        }


        protected void AddSymbol(TEnum _enum, TRecord record)
        {
            recordDict.Add(_enum.ToString(),record);
        }

        protected abstract void FillDictionary();
        public abstract IList<TRecord> GetRecords();
    }
}
