using CADKit.Proxy;
using System.Collections.Generic;

namespace CADKitBasic.Services
{
    public class SystemVariableService
    {
        public static Dictionary<string,object> StoreSystemVariables()
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            result.Add("CLayer", (string)CADProxy.GetSystemVariable("CLayer"));

            return result;
        }

        public static void RestoreSystemVariables(Dictionary<string, object> _variables)
        {
            CADProxy.SetSystemVariable("CLayer", _variables["CLayer"]);
        }
    }
}
