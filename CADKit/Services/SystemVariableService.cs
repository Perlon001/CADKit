using CADKit.ServiceCAD;
using CADKitCore.Model;

namespace CADKitCore.Services
{
    public class SystemVariableService
    {
        public static SystemVariables GetSystemVariables()
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
