using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZwSoft.ZwCAD.DatabaseServices;
using ZwSoft.ZwCAD.Runtime;
using ZwSoft.ZwCAD.Windows;
using ZwSoft.ZwCAD.ApplicationServices;
using ZwSoft.ZwCAD.EditorInput;
using ZwSoft.ZwCAD.Geometry;

namespace CADKit
{
    public class CommandXDataContextMenuTest
    {
        [CommandMethod("ReadXData")]
        public static void ReadXData_Method()
        {
            const string TestAppName = "e-cad Zelbet";

            Database db = HostApplicationServices.WorkingDatabase;
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            try
            {
                PromptEntityResult prEntRes = ed.GetEntity("Select an Entity");
                if (prEntRes.Status == PromptStatus.OK)
                {
                    using (Transaction tr = db.TransactionManager.StartTransaction())
                    {
                        Entity ent = (Entity)tr.GetObject(prEntRes.ObjectId, OpenMode.ForRead);
                        ResultBuffer rb = ent.GetXDataForApplication(TestAppName);
                        var aaa = ent.XData;
                        if (rb != null)
                        {
                            TypedValue[] rvArr = rb.AsArray();
                            foreach (TypedValue tv in rvArr)
                            {
                                switch ((DxfCode)tv.TypeCode)
                                {
                                    case DxfCode.ExtendedDataRegAppName:
                                        string appName = (string)tv.Value;
                                        ed.WriteMessage("\nXData of appliation name (1001) {0}:", appName);
                                        break;
                                    case DxfCode.ExtendedDataAsciiString:
                                        string asciiStr = (string)tv.Value;
                                        ed.WriteMessage("\n\tAscii string (1000): {0}", asciiStr);
                                        break;
                                    case DxfCode.ExtendedDataLayerName:
                                        string layerName = (string)tv.Value;
                                        ed.WriteMessage("\n\tLayer name (1003): {0}", layerName);
                                        break;
                                    case DxfCode.ExtendedDataBinaryChunk:
                                        Byte[] chunk = tv.Value as Byte[];
                                        string chunkText = Encoding.ASCII.GetString(chunk);
                                        ed.WriteMessage("\n\tBinary chunk (1004): {0}", chunkText);
                                        break;
                                    case DxfCode.ExtendedDataHandle:
                                        ed.WriteMessage("\n\tObject handle (1005): {0}", tv.Value);
                                        break;
                                    case DxfCode.ExtendedDataXCoordinate:
                                        Point3d pt = (Point3d)tv.Value;
                                        ed.WriteMessage("\n\tPoint (1010): {0}", pt.ToString());
                                        break;
                                    case DxfCode.ExtendedDataWorldXCoordinate:
                                        Point3d pt1 = (Point3d)tv.Value;
                                        ed.WriteMessage("\n\tWorld point (1011): {0}", pt1.ToString());
                                        break;
                                    case DxfCode.ExtendedDataWorldXDisp:
                                        Point3d pt2 = (Point3d)tv.Value;
                                        ed.WriteMessage("\n\tDisplacement (1012): {0}", pt2.ToString());
                                        break;
                                    case DxfCode.ExtendedDataWorldXDir:
                                        Point3d pt3 = (Point3d)tv.Value;
                                        ed.WriteMessage("\n\tDirection (1013): {0}", pt3.ToString());
                                        break;
                                    case DxfCode.ExtendedDataControlString:
                                        string ctrStr = (string)tv.Value;
                                        ed.WriteMessage("\n\tControl string (1002): {0}", ctrStr);
                                        break;
                                    case DxfCode.ExtendedDataReal:
                                        double realValue = (double)tv.Value;
                                        ed.WriteMessage("\n\tReal (1040): {0}", realValue);
                                        break;
                                    case DxfCode.ExtendedDataDist:
                                        double dist = (double)tv.Value;
                                        ed.WriteMessage("\n\tDistance (1041): {0}", dist);
                                        break;
                                    case DxfCode.ExtendedDataScale:
                                        double scale = (double)tv.Value;
                                        ed.WriteMessage("\n\tScale (1042): {0}", scale);
                                        break;
                                    case DxfCode.ExtendedDataInteger16:
                                        Int16 int16 = (short)tv.Value;
                                        ed.WriteMessage("\n\tInt16 (1070): {0}", int16);
                                        break;
                                    case DxfCode.ExtendedDataInteger32:
                                        Int32 int32 = (Int32)tv.Value;
                                        ed.WriteMessage("\n\tInt32 (1071): {0}", int32);
                                        break;
                                    default:
                                        ed.WriteMessage("\n\tUnknown XData DXF code.");
                                        break;
                                }
                            }
                        }
                        else
                            ed.WriteMessage("The entity does not have the {0} XData.", TestAppName);

                        tr.Commit();
                    }
                }
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(ex.ToString());
            }
        }

        [CommandMethod("ContextMenuExtTest")]
        static public void ContextMenuExtTest()
        {
            ContextMenuExtension contectMenu = new ContextMenuExtension();
            MenuItem item0 = new MenuItem("Line context menu");
            contectMenu.MenuItems.Add(item0);
            MenuItem Item1 = new MenuItem("Test1");
            Item1.Click += new EventHandler(Test1_Click);
            item0.MenuItems.Add(Item1);
            MenuItem Item2 = new MenuItem("Test2");
            Item2.Click += new EventHandler(Test2_Click);
            item0.MenuItems.Add(Item2);
            //for custom entity, replace the "Line" with .NET
            //(managed) wrapper of custom entity
            Application.AddObjectContextMenuExtension(Polyline.GetClass(typeof(Line)), contectMenu);
        }

        static void Test1_Click(object sender, EventArgs e)
        {
            Application.ShowAlertDialog("Test1 clicked\n");
        }

        static void Test2_Click(object sender, EventArgs e)
        {
            Application.ShowAlertDialog("Test2 clicked\n");
        }


        [CommandMethod("XDataContextMenuTest")]
        static public void XDataContextMenuTest()
        {
            ContextMenuExtension contextMenu = new ContextMenuExtension();
            MenuItem item0 = new MenuItem("E-CAD menu kontekstowe");
            contextMenu.MenuItems.Add(item0);
            MenuItem Item1 = new MenuItem("E-CAD coś tam");
            Item1.Click += new EventHandler(Item1_Click);
            item0.MenuItems.Add(Item1);
            MenuItem Item2 = new MenuItem("E-CAD inne coś");
            Item2.Click += new EventHandler(Item2_Click);
            item0.MenuItems.Add(Item2);
            //ADD the popup item
            contextMenu.Popup += new EventHandler(contextMenu_Popup);
            //for custom entity, replace the "Line" with .NET
            //(managed) wrapper of custom entity
            Application.AddObjectContextMenuExtension(Polyline.GetClass(typeof(Polyline)), contextMenu);
        }

        static void Item1_Click(object sender, EventArgs e)
        {
            Application.ShowAlertDialog("Item 1 clicked\n");
        }

        static void Item2_Click(object sender, EventArgs e)
        {
            Application.ShowAlertDialog("Item 2 clicked\n");
        }

        static void contextMenu_Popup(object sender, EventArgs e)
        {
            try
            {
                ContextMenuExtension contextMenu = sender as ContextMenuExtension;
                if (contextMenu != null)
                {
                    Document doc = Application.DocumentManager.MdiActiveDocument;
                    Editor ed = doc.Editor;
                    // This is the "Root context menu" item
                    MenuItem rootItem = contextMenu.MenuItems[0];
                    PromptSelectionResult acSSPrompt = ed.SelectImplied();
                    bool bEnable = false;
                    if (acSSPrompt.Status == PromptStatus.OK)
                    {
                        SelectionSet set = acSSPrompt.Value;
                        ObjectId[] ids = set.GetObjectIds();
                        //if only one selected
                        if (ids.Length == 1)
                        {
                            //get the selection
                            ObjectId id = ids[0];
                            Database db = doc.Database;
                            using (Transaction tx = db.TransactionManager.StartTransaction())
                            {
                                DBObject ent = tx.GetObject(id, OpenMode.ForRead);
                                using (ResultBuffer result = ent.GetXDataForApplication("e-cad Zelbet"))
                                {
                                    if (result != null)
                                    {
                                        bEnable = true;
                                    }
                                }
                                tx.Commit();
                            }
                        }
                    }
                    rootItem.Enabled = bEnable;
                }
            }
            catch
            {
            }
        }
    }
}
