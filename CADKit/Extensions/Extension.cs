using CADKit.ServiceCAD;
using CADKitCore.Settings;
using System;
using System.Collections;
using System.Collections.Generic;
using ZwSoft.ZwCAD.DatabaseServices;

namespace CADKitCore.Extensions
{
    public static class Extension
    {
        public static void UsingTransaction(Action<Transaction> action)
        {
            using (var tr = CADProxy.Database.TransactionManager.StartTransaction())
            {
                try
                {
                    action(tr);
                    tr.Commit();
                }
                catch (Exception ex)
                {
                    tr.Abort();
                    throw ex;
                }
            }
        }

        public static void UsingTransaction(this Database database, Action<Transaction> action)
        {
            using (var tr = database.TransactionManager.StartTransaction())
            {
                try
                {
                    action(tr);
                    tr.Commit();
                }
                catch (Exception)
                {
                    tr.Abort();
                    throw;
                }
            }
        }

        public static Dictionary<string, string> GetCustomProperties(this Database db)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            IDictionaryEnumerator dictEnum = db.SummaryInfo.CustomProperties;
            while (dictEnum.MoveNext())
            {
                DictionaryEntry entry = dictEnum.Entry;
                result.Add((string)entry.Key, (string)entry.Value);
            }
            return result;
        }

    }
}
