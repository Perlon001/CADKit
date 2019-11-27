using System;
using System.Collections;
using System.Collections.Generic;

#if ZwCAD
using ZwSoft.ZwCAD.ApplicationServices;
using CADApplicationServices = ZwSoft.ZwCAD.ApplicationServices;
using ZwSoft.ZwCAD.EditorInput;
using ZwSoft.ZwCAD.DatabaseServices;
using CADDatabaseServices = ZwSoft.ZwCAD.DatabaseServices;
#endif

#if AutoCAD
using Autodesk.AutoCAD.ApplicationServices;
using CADApplicationServices = Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.DatabaseServices;
using CADDatabaseServices = Autodesk.AutoCAD.DatabaseServices;
#endif



namespace CADProxy
{
    public delegate void SystemVariableChangedEventHandler(object sender, CADApplicationServices.SystemVariableChangedEventArgs e);
    
    public class ProxyCAD
    { 
        public static void UsingTransaction(Action<Transaction> action)
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

        public static void UsingTransaction(Database database, Action<Transaction> action)
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
            DatabaseSummaryInfoBuilder infoBuilder = new DatabaseSummaryInfoBuilder(Database.SummaryInfo);
            IDictionary custProps = infoBuilder.CustomPropertyTable;
            if (custProps.Contains(key))
                custProps[key] = value;
            else
                custProps.Add(key, value);
            Database.SummaryInfo = infoBuilder.ToDatabaseSummaryInfo();
        }

        public static string GetCustomProperty(string key)
        {
            DatabaseSummaryInfoBuilder sumInfo = new DatabaseSummaryInfoBuilder(Database.SummaryInfo);
            IDictionary custProps = sumInfo.CustomPropertyTable;
            if (!custProps.Contains(key))
                custProps.Add(key, "");

            return (string)custProps[key];
        }

        public static void WriteMessage(string message)
        {
            Editor.WriteMessage(message);
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

        public static event CommandEventHandler CommandEnded
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

        public static event DocumentCollectionEventHandler DocumentActivated
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

        public static event DocumentDestroyedEventHandler DocumentDestroyed
        {
            add
            {
                DocumentManager.DocumentDestroyed += value;
            }
            remove
            {
                DocumentManager.DocumentDestroyed -= value;
            }
        }

        public static event DrawingOpenEventHandler EndDwgOpen
        {
            add
            {
                Document.EndDwgOpen += value;
            }
            remove
            {
                Document.EndDwgOpen -= value;
            }
        }

        public static event DocumentCollectionEventHandler DocumentCreated
        {
            add
            {
                DocumentManager.DocumentCreated += value;
            }
            remove
            {
                DocumentManager.DocumentCreated -= value;
            }
        }

        public static event CADApplicationServices.SystemVariableChangedEventHandler SystemVariableChanged
        {
            add
            {
                Application.SystemVariableChanged += value;
            }
            remove
            {
                Application.SystemVariableChanged -= value;
            }
        }

        public static void ShowAlertDialog(string message)
        {
            Application.ShowAlertDialog(message);
        }

        public static object GetSystemVariable(string name)
        {
            return Application.GetSystemVariable(name);
        }

        public static void SetSystemVariable(string name, object value)
        {
            Application.SetSystemVariable(name, value);
        }

        public static DocumentCollection DocumentManager
        {
            get { return Application.DocumentManager; }
        }

        public static Document Document
        {
            get { return Application.DocumentManager.MdiActiveDocument; }
        }

        public static Database Database
        {
            get { return Document.Database; }
        }

        public static Editor Editor
        {
            get { return Document.Editor; }
        }

        public static string Product
        {
            get { return (string)Application.GetSystemVariable("PRODUCT"); }
        }
        public static object ApplicationObject
        {
            get 
            {
                #if ZwCAD
                return Application.ZcadApplication;
                #endif
                #if AutoCAD
                return Application.AcadApplication;
                #endif
            }
        }
    }
}
