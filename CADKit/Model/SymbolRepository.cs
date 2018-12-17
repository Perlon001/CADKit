using CADKitCore.Contract;
using System;
using System.Collections.Generic;
using ZwSoft.ZwCAD.DatabaseServices;

namespace CADKitCore.Model
{
    public abstract class SymbolRepository<TEnum,TRecord> : ISymbolRepository where TRecord : SymbolTableRecord
    {
        private Dictionary<string, TRecord> recordDict;

        public SymbolRepository()
        {
            recordDict = new Dictionary<string, TRecord>();
            FillDictionary();
        }

        public SymbolTableRecord GetSymbol<T>(T _enum)
        {
            Dictionary<T,string> a = ToDictionary<T>();
            string b = a[_enum];
            return recordDict[b];
        }

        protected void AddSymbol(TEnum _enum, TRecord record)
        {
            recordDict.Add(_enum.ToString(),record);
        }

        protected Dictionary<T, string> ToDictionary<T>()
        {
            Type enumType = typeof(T);

            if (!enumType.IsEnum)
                throw new ArgumentException("Argument nie jest typem wyliczeniowym");

            var values = (T[])Enum.GetValues(enumType);

            var dictionary = new Dictionary<T, string>();

            foreach (var value in values)
            {
                dictionary.Add(value, Enum.GetName(enumType, value));
            }

            return dictionary;
        }

        protected abstract void FillDictionary();

    }
}
