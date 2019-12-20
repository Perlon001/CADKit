using CADKit.Models;
using CADProxy;

namespace CADKit.Services
{
    public class SystemVariableService
    {
        public static SystemVariables GetActualSystemVariables()
        {
            SystemVariables variables = new SystemVariables();
            variables.CLayer = (string)ProxyCAD.GetSystemVariable("CLayer");

            return variables;
        }

        public static void RestoreSystemVariables(SystemVariables variables)
        {
            ProxyCAD.SetSystemVariable("CLayer", variables.CLayer);
        }
    }
}
