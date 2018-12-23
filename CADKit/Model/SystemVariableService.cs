using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZwSoft.ZwCAD.ApplicationServices;

namespace CADKitCore.Model
{
    public class SystemVariableService
    {
        public static SystemVariables GetSystemVariables()
        {
            SystemVariables variables = new SystemVariables();
            variables.CLayer = (string)Application.GetSystemVariable("CLayer");

            return variables;
        }

        public static void RestoreSystemVariables(SystemVariables variables)
        {
            Application.SetSystemVariable("CLayer", variables.CLayer);
        }

    }
}
