using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZwSoft.ZwCAD.DatabaseServices
{
    public static class CADDatabaseExtensions
    {
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

        public static string GetCustomProperty(this Database db, string key)
        {
            DatabaseSummaryInfoBuilder sumInfo = new DatabaseSummaryInfoBuilder(db.SummaryInfo);
            IDictionary custProps = sumInfo.CustomPropertyTable;
            if (!custProps.Contains(key))
                custProps.Add(key, "");

            return (string)custProps[key];
        }

        public static void SetCustomProperty(this Database db, string key, string value)
        {
            DatabaseSummaryInfoBuilder infoBuilder = new DatabaseSummaryInfoBuilder(db.SummaryInfo);
            IDictionary custProps = infoBuilder.CustomPropertyTable;
            if (custProps.Contains(key))
                custProps[key] = value;
            else
                custProps.Add(key, value);
            db.SummaryInfo = infoBuilder.ToDatabaseSummaryInfo();
        }
    }
}
