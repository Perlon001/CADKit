using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using CADKit.Models;
using ZwSoft.ZwCAD.Windows;

#if ZwCAD
using ZwSoft.ZwCAD.ApplicationServices;
using CADApplicationServices = ZwSoft.ZwCAD.ApplicationServices;
using ZwSoft.ZwCAD.EditorInput;
using ZwSoft.ZwCAD.DatabaseServices;
using ZwSoft.ZwCAD.Geometry;
#endif

#if AutoCAD
using Autodesk.AutoCAD.ApplicationServices;
using CADApplicationServices = Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
#endif

namespace CADKit.Proxy
{  
    public class CADProxy
    { 
        public object cadApplication { get; }

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

        public static object GetSystemVariable(string name)
        {
            return Application.GetSystemVariable(name);
        }

        public static void SetSystemVariable(string name, object value)
        {
            Application.SetSystemVariable(name, value);
        }

        public static void WriteMessage(string message)
        {
            Editor.WriteMessage(message);
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

        public static BlockTableRecord GetBlockTableRecord(Transaction transaction, OpenMode mode)
        {
            var blockTable = transaction.GetObject(Database.BlockTableId, OpenMode.ForRead) as BlockTable;
            if (Database.TileMode == false && IsInLayoutPaper() == true)
            {
                return transaction.GetObject(blockTable[BlockTableRecord.PaperSpace], mode) as BlockTableRecord;
            }
            else
            {
                return transaction.GetObject(blockTable[BlockTableRecord.ModelSpace], mode) as BlockTableRecord;
            }
        }

        public static DocumentCollection DocumentManager
        {
            get { return Application.DocumentManager; }
        }

        public static Window MainWindow
        {
            get { return Application.MainWindow; }
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

        public static object objectApplication
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

        public static void ZoomExtens()
        {
            objectApplication.GetType().InvokeMember("ZoomExtents", BindingFlags.InvokeMethod, null, objectApplication, null);
        }

        public static void ShowAlertDialog(string message)
        {
            Application.ShowAlertDialog(message);
        }

        public static bool IsInLayoutPaper()
        {
            if (Database.TileMode)
                return false;
            else
            {
                if (Database.PaperSpaceVportId == ObjectId.Null)
                    return false;
                else if (Editor.CurrentViewportObjectId == ObjectId.Null)
                    return false;
                else if (Editor.CurrentViewportObjectId == Database.PaperSpaceVportId)
                    return true;
                else
                    return false;
            }
        }

        public static Point3d[] GetTextArea(DBText text)
        {
            Point3d leftBottom = text.GeometricExtents.MinPoint;
            Point3d rightTop = text.GeometricExtents.MaxPoint;

            return new Point3d[2] { leftBottom, rightTop };
        }

        public static DBText ToDBText(AttributeDefinition att)
        {
            DBText result = new DBText();
            result.Annotative = att.Annotative;
            result.CastShadows = att.CastShadows;
            result.Color = att.Color;
            result.ColorIndex = att.ColorIndex;
            //result.EdgeStyleId = att.EdgeStyleId;
            //result.FaceStyleId = att.FaceStyleId;
            //result.ForceAnnoAllVisible = att.ForceAnnoAllVisible;
            result.HasSaveVersionOverride = att.HasSaveVersionOverride;
            result.Height = att.Height;
            result.HorizontalMode = att.HorizontalMode;
            result.IsMirroredInX = att.IsMirroredInX;
            result.IsMirroredInY = att.IsMirroredInY;
            result.Layer = att.Layer;
            result.LayerId = att.LayerId;
            result.Linetype = att.Linetype;
            result.LinetypeId = att.LinetypeId;
            result.LinetypeScale = att.LinetypeScale;
            result.LineWeight = att.LineWeight;
            result.Material = att.Material;
            result.MaterialId = att.MaterialId;
            //result.MaterialMapper = att.MaterialMapper;
            //result.MergeStyle = att.MergeStyle;
            result.Normal = att.Normal;
            result.Oblique = att.Oblique;
            //result.PlotStyleName = att.PlotStyleName;
            //result.PlotStyleNameId = att.PlotStyleNameId;
            
            result.Position = att.Position;
            result.Justify = att.Justify;
            result.AlignmentPoint = att.AlignmentPoint;
            
            result.ReceiveShadows = att.ReceiveShadows;
            result.Rotation = att.Rotation;
            result.TextString = att.TextString;
            result.TextStyle = att.TextStyle;
            result.TextStyleId = att.TextStyleId;
            result.Thickness = att.Thickness;
            result.Transparency = att.Transparency;
            result.VerticalMode = att.VerticalMode;
            result.Visible = att.Visible;
            //result.VisualStyleId = att.VisualStyleId;
            result.WidthFactor = att.WidthFactor;
            //result.XData = att.XData;

            return result;
        }

        public static void GroupDelete(Group _group)
        {
            using(var tr = Document.TransactionManager.StartTransaction())
            {
                var gr = _group.ObjectId.GetObject(OpenMode.ForWrite) as Group;
                foreach (var id in gr.GetAllEntityIds())
                {
                    if (!id.IsErased)
                    {
                        tr.GetObject(id, OpenMode.ForWrite).Erase();
                    }
                }
                gr.Erase(true);
                tr.Commit();
            }
        }

        public static void CancelRunningCommand()
        {
            string cmds = GetSystemVariable("CMDNAMES") as string;
            string esc = string.Empty;

            if (cmds.Length > 0)
            {
                // case of a command line command to be cancelled
                int cmdNum = cmds.Split('\\').Length;
                for (int i = 0; i < cmdNum; i++)
                {
                    esc += "\x03";
                }
                Document.SendStringToExecute(esc, true, false, true);
            }
            else if (!Editor.IsQuiescent)
            {
                // case of a prompt to be cancelled
                esc = "\x03";
                Document.SendStringToExecute(esc, true, false, true);
            }
            else
            {
                // nothing to cancel                
            }
        }

    }
}
