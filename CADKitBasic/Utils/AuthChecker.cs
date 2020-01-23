using System.Diagnostics;
using ZwSoft.ZwCAD.ApplicationServices;

namespace CADKitBasic.Utils
{
    public static class AuthChecker
    {
        public static bool Authorise(object sender)
        {
            var st = new StackTrace();
            var sf = st.GetFrame(1).GetMethod();
            var sn = sf.DeclaringType.FullName+"."+sf.Name;
            // Application.ShowAlertDialog($"Autoryzacja metody {sn}");

            return true;
        }
    }
}
