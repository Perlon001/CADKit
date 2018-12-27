using CADKit.ServiceCAD.Interface;
using CADKit.ServiceCAD.Proxy;
using System;
using System.Collections;
using System.Collections.Generic;

namespace CADKit.ServiceCAD
{
    public delegate void SystemVariableChangedEventHandler(object sender, SystemVariableChangedEventArgs e);
    
    public class CADProxy
    {
        public static IEntityTypeFactory GetEntityFactory()
        {
            switch(CADEnvironment.Instance.Platform)
            {
                case CADPlatforms.AutoCAD:
                    return new Proxy.AutoCAD.EntityTypeFactory();
                case CADPlatforms.ZwCAD:
                    return new Proxy.ZwCAD.EntityTypeFactory();
                default:
                    throw new NotImplementedException();
            }
        }

        public static void UsingTransaction(Action<ZwSoft.ZwCAD.DatabaseServices.Transaction> action)
        {
            using (var tr = Database.TransactionManager.StartTransaction())
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

        public static void UsingTransaction(ZwSoft.ZwCAD.DatabaseServices.Database database, Action<ZwSoft.ZwCAD.DatabaseServices.Transaction> action)
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

        public static void SetCustomProperty(string key, string value)
        {
            ZwSoft.ZwCAD.DatabaseServices.DatabaseSummaryInfoBuilder infoBuilder = new ZwSoft.ZwCAD.DatabaseServices.DatabaseSummaryInfoBuilder(Database.SummaryInfo);
            IDictionary custProps = infoBuilder.CustomPropertyTable;
            if (custProps.Contains(key))
                custProps[key] = value;
            else
                custProps.Add(key, value);
            Database.SummaryInfo = infoBuilder.ToDatabaseSummaryInfo();
        }

        public static string GetCustomProperty(string key)
        {
            ZwSoft.ZwCAD.DatabaseServices.DatabaseSummaryInfoBuilder sumInfo = new ZwSoft.ZwCAD.DatabaseServices.DatabaseSummaryInfoBuilder(Database.SummaryInfo);
            IDictionary custProps = sumInfo.CustomPropertyTable;
            if (!custProps.Contains(key))
                custProps.Add(key, "");

            return (string)custProps[key];
        }

        public static Dictionary<string, string> GetCustomProperties()
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            IDictionaryEnumerator dictEnum = Database.SummaryInfo.CustomProperties;
            while (dictEnum.MoveNext())
            {
                DictionaryEntry entry = dictEnum.Entry;
                result.Add((string)entry.Key, (string)entry.Value);
            }
            return result;
        }


        public static event ZwSoft.ZwCAD.ApplicationServices.CommandEventHandler CommandEnded
        {
            add
            {
                Document.CommandEnded += value;
            }
            remove
            {
                Document.CommandEnded -= value;
            }
        }

        public static event ZwSoft.ZwCAD.ApplicationServices.DocumentCollectionEventHandler DocumentActivated
        {
            add
            {
                DocumentManager.DocumentActivated += value;
            }
            remove
            {
                DocumentManager.DocumentActivated -= value;
            }
        }

        public static event ZwSoft.ZwCAD.ApplicationServices.SystemVariableChangedEventHandler SystemVariableChanged
        {
            add
            {
                ZwSoft.ZwCAD.ApplicationServices.Application.SystemVariableChanged += value;
            }
            remove
            {
                ZwSoft.ZwCAD.ApplicationServices.Application.SystemVariableChanged -= value;
            }
        }

        public static void ShowAlertDialog(string message)
        {
            ZwSoft.ZwCAD.ApplicationServices.Application.ShowAlertDialog(message);
        }

        public static object GetSystemVariable(string name)
        {
            return ZwSoft.ZwCAD.ApplicationServices.Application.GetSystemVariable(name);
        }

        public static void SetSystemVariable(string name, object value)
        {
            ZwSoft.ZwCAD.ApplicationServices.Application.SetSystemVariable(name, value);
        }

        public static ZwSoft.ZwCAD.ApplicationServices.DocumentCollection DocumentManager
        {
            get { return ZwSoft.ZwCAD.ApplicationServices.Application.DocumentManager; }
        }

        public static ZwSoft.ZwCAD.ApplicationServices.Document Document
        {
            get { return ZwSoft.ZwCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument; }
        }

        public static ZwSoft.ZwCAD.DatabaseServices.Database Database
        {
            get { return Document.Database; }
        }

        public static ZwSoft.ZwCAD.EditorInput.Editor Editor
        {
            get { return Document.Editor; }
        }

    }
}
