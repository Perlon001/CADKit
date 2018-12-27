using CADKit.ServiceCAD.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// using ZwSoft.ZwCAD.ApplicationServices;
// using ZwSoft.ZwCAD.EditorInput;
// using ZwSoft.ZwCAD.DatabaseServices;
using CADKit.ServiceCAD.Proxy;

namespace CADKit.ServiceCAD
{
    public delegate void SystemVariableChangedEventHandler(object sender, SystemVariableChangedEventArgs e);

    public class CADProxy
    {
        public event ZwSoft.ZwCAD.ApplicationServices.DocumentCollectionEventHandler DocumentActivated
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

        public static event SystemVariableChangedEventHandler _SystemVariableChanged
        {
            add
            {
                _SystemVariableChanged += value;
            }
            remove
            {
                _SystemVariableChanged -= value;
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
