using CADKit.Models;
using CADProxy;

namespace CADKit.Services
{
    public class SystemVariableService
    {
        public static SystemVariables GetSystemVariables()
        {
            SystemVariables variables = new SystemVariables();
            variables.CLayer = (string)CADProxy.ProxyCAD.GetSystemVariable("CLayer");
            // throw new Exception();

            return variables;
        }

        public static void RestoreSystemVariables(SystemVariables variables)
        {
            CADProxy.ProxyCAD.SetSystemVariable("CLayer", variables.CLayer);
        }

    }
}
