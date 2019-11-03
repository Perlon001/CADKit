using CADKit.ServiceCAD.Interface;
using CADKit.ServiceCAD.Proxy;
using System;
using System.Collections;
using System.Collections.Generic;
#if ZwCAD
using ApplicationServices = ZwSoft.ZwCAD.ApplicationServices;
using EditorInput = ZwSoft.ZwCAD.EditorInput;
using ZwSoft.ZwCAD.DatabaseServices;
#endif
#if AutoCAD
using ApplicationServices = Autodesk.AutoCAD.ApplicationServices;
using EditorInput = Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.DatabaseServices;
#endif

namespace CADKit.ServiceCAD
{
    public delegate void SystemVariableChangedEventHandler(object sender, SystemVariableChangedEventArgs e);
    
    public class CADProxy
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

        public static event ApplicationServices.CommandEventHandler CommandEnded
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

        public static event ApplicationServices.DocumentCollectionEventHandler DocumentActivated
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

        public static event ApplicationServices.DocumentDestroyedEventHandler DocumentDestroyed
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

        public static event ApplicationServices.DrawingOpenEventHandler EndDwgOpen
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

        public static event ApplicationServices.DocumentCollectionEventHandler DocumentCreated
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

        public static event ApplicationServices.SystemVariableChangedEventHandler SystemVariableChanged
        {
            add
            {
                ApplicationServices.Application.SystemVariableChanged += value;
            }
            remove
            {
                ApplicationServices.Application.SystemVariableChanged -= value;
            }
        }

        public static void ShowAlertDialog(string message)
        {
            ApplicationServices.Application.ShowAlertDialog(message);
        }

        public static object GetSystemVariable(string name)
        {
            return ApplicationServices.Application.GetSystemVariable(name);
        }

        public static void SetSystemVariable(string name, object value)
        {
            ApplicationServices.Application.SetSystemVariable(name, value);
        }

        public static ApplicationServices.DocumentCollection DocumentManager
        {
            get { return ApplicationServices.Application.DocumentManager; }
        }

        public static ApplicationServices.Document Document
        {
            get { return ApplicationServices.Application.DocumentManager.MdiActiveDocument; }
        }

        public static Database Database
        {
            get { return Document.Database; }
        }

        public static EditorInput.Editor Editor
        {
            get { return Document.Editor; }
        }

    }
}
