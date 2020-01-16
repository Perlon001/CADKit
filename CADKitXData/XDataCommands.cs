using CADKit.Models;
using System;
using System.Collections.Generic;
using CADKit.Utils;
using CADProxy;
using CADProxy.Runtime;

#if ZwCAD
using ZwSoft.ZwCAD.EditorInput;
using ZwSoft.ZwCAD.DatabaseServices;
#endif

#if AutoCAD
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.DatabaseServices;
#endif

namespace CADKitXData
{
    public class XDataCommands
    {
        private string appName = "e-cad Zelbet";

        [CommandMethod("CK_XDAPPLIST")]
        public void GetXDataApplicationList()
        {
            var doc = ProxyCAD.Document;
            using( var tr = doc.TransactionManager.StartTransaction())
            {
                var rat = tr.GetObject(doc.Database.RegAppTableId, OpenMode.ForRead) as RegAppTable;
                foreach(var id in rat)
                {
                    var ratr = tr.GetObject(id, OpenMode.ForRead) as RegAppTableRecord;
                    ProxyCAD.Editor.WriteMessage("\n"+ratr.Name);
                }
            }
        }
    }
}
