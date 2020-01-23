using CADKitBasic.Models;
using CADKit;
using CADKit.Proxy;

namespace CADKitBasic.Services
{
    public class SystemVariableService
    {
        public static SystemVariables GetActualSystemVariables()
        {
            SystemVariables variables = new SystemVariables();
            variables.CLayer = (string)CADProxy.GetSystemVariable("CLayer");

            return variables;
        }

        public static void RestoreSystemVariables(SystemVariables variables)
        {
            CADProxy.SetSystemVariable("CLayer", variables.CLayer);
        }
    }
}
