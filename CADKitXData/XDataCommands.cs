//using CADKit.Runtime;
//using CADKit.Proxy;

//#if ZwCAD
//using ZwSoft.ZwCAD.DatabaseServices;
//#endif

//#if AutoCAD
//using Autodesk.AutoCAD.EditorInput;
//using Autodesk.AutoCAD.DatabaseServices;
//#endif

//namespace CADKitXData
//{
//    public class XDataCommands
//    {
//        private readonly string appName = "e-cad Zelbet";

//        [CommandMethod("CK_XDAPPLIST")]
//        public void GetXDataApplicationList()
//        {
//            var doc = CADProxy.Document;
//            using( var tr = doc.TransactionManager.StartTransaction())
//            {
//                var rat = tr.GetObject(doc.Database.RegAppTableId, OpenMode.ForRead) as RegAppTable;
//                foreach(var id in rat)
//                {
//                    var ratr = tr.GetObject(id, OpenMode.ForRead) as RegAppTableRecord;
//                    CADProxy.Editor.WriteMessage("\n"+ratr.Name);
//                }
//            }
//        }
//    }
//}
