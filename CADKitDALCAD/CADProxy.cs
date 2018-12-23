using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZwSoft.ZwCAD.ApplicationServices;
using ZwSoft.ZwCAD.EditorInput;
using ZwSoft.ZwCAD.DatabaseServices;

namespace CADKitDALCAD
{
    public class CADProxy
    {
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
    }
}
